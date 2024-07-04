using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��Ʈ����Ʈ ������ �� ���� �����ؾ��ҵ�. ����� �ش� ������Ʈ�� ���ӿ�����Ʈ�� �������� �����Ѵ�.
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
        // ���� �浹���� �ʾ����� �˻�
        if (other.transform.GetComponentInParent<Unit>() == null)
        {
            return;
        }
        // �浹ü�� �����Ͻ�
        Unit unit = other.transform.GetComponentInParent<Unit>();

        if (unit.Team != _myTeam)
        {
            isBumpedWithUnit = true;
            Debug.Log(name + "�ĸ���");
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
        // ���� �浹���� �ʾ����� �˻�
        if (other.transform.GetComponentInParent<Unit>() == null)
        {
            return;
        }
        // �浹ü�� �����Ͻ�
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


    /// <summary> ������Ʈ �ı��ϰ� ��ƼŬ ���� </summary>
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
