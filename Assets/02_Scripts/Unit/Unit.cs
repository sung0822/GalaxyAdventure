using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 어트리뷰트 문법을 잘 몰라서 공부해야할듯. 기능은 해당 컴포넌트를 게임오브젝트가 가지도록 강제한다.
/// </summary>
[RequireComponent (typeof (AudioSource))]
public abstract class Unit : MonoBehaviour, ITeamMember
{
    public int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    protected int _maxHp;
    public int currentHp { get { return _currentHp; } set { _currentHp = value; } }
    protected int _currentHp;

    public int power { get { return _power; } set { _power = value; } }
    public TeamType Team { get { return _myTeam; } set { _myTeam = value; } }
    protected TeamType _myTeam;
    public bool isImmortal { get { return _isImmortal; }}
    protected bool _isImmortal;

    protected int _power;

    bool isBumpedWithUnit;
    float timeAfterBumped;

    protected AudioSource audioSource;
    protected AudioClip audioClip;

    public abstract bool isAttacking { get; set; }

    protected virtual void Start()
    {
        _power = 10;
        maxHp = 100;
        currentHp = 100;

        transform.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Sounds/ExplosionSound");
    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        
        if (isBumpedWithUnit)
        {
            return;
        }
        // 적과 충돌하지 않았을시 검사
        if (other.transform.GetComponentInParent<Unit>() == null)
        {
            return;
        }
        // 충돌체가 유닛일시
        Unit unit = other.transform.GetComponentInParent<Unit>();

        if (unit.Team != _myTeam)
        {
            isBumpedWithUnit = true;
            Debug.Log(name + "쳐맞음");
            unit.Hit(30);
            return;
        }
    }

    

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (isBumpedWithUnit)
        {
            return;
        }
        // 적과 충돌하지 않았을시 검사
        if (other.transform.GetComponentInParent<Unit>() == null)
        {
            return;
        }
        // 충돌체가 유닛일시
        if (other.transform.GetComponentInParent<Unit>()?.gameObject.layer == LayerMask.NameToLayer("UNIT"))
        {

            Unit unit = other.transform.GetComponentInParent<Unit>();

            if (unit.Team != _myTeam)
            {
                unit.Hit(30);
                isBumpedWithUnit = true;
                SetIsBumped(false, 3.0f);
                return;
            }
            else
            {

            }
        }
    }
    protected virtual void OnCollisionStay(Collision collision)
    {
        
    }

    public virtual void Hit(int damage)
    {
        if (isImmortal)
        {
            return;
        }
        currentHp -= damage;
        CheckDead();
    }

    protected IEnumerator SetIsBumped(bool isBumpedWithUnit, float time)
    {
        yield return new WaitForSeconds(time);
        this.isBumpedWithUnit = isBumpedWithUnit;
    }

    public virtual IEnumerator SetImmortal(bool isImmortal, float time)
    {
        yield return new WaitForSeconds(time);
        this._isImmortal = isImmortal;
    }
    public virtual void SetImmortal(bool isImmortal)
    {
        this._isImmortal = isImmortal;
    }

    protected virtual void CheckDead()
    {
        if (currentHp <= 0)
        {
            audioSource.clip = audioClip;
            audioSource.enabled = true;
            audioSource.PlayOneShot(audioClip, 1.0f);
            Die();
        }
    }


    /// <summary> 오브젝트 파괴하고 파티클 생성 </summary>
    protected virtual void Die()
    {
        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.unitExplodingParticle, transform.position, transform.rotation);

        particle.transform.localScale = this.transform.localScale * 0.1f;
        Destroy(particle, 1.5f);

        Destroy(this.gameObject);
    }

    protected virtual void LateUpdate()
    {
    }




}
