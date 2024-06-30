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
    public bool isImmortal { get { return _isImmortal; } set { _isImmortal = value; } }
    bool _isImmortal;

    protected float immortalTime;

    protected int _power;

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

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!_isImmortal)
        {
            return;
        }

        immortalTime -= Time.deltaTime;
        if (immortalTime <= 0)
        {
            isImmortal = false;
        }
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if(other.transform.GetComponentInParent<Unit>()?.gameObject.layer == LayerMask.NameToLayer("UNIT"))
        {

            Unit unit = other.transform.GetComponentInParent<Unit>();
            
            if (unit.Team != _myTeam)
            {
                currentHp -= 30;
                return;
            }
            else
            {

            }
        }
    }

    

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }
    protected virtual void OnCollisionStay(Collision collision)
    {
        
    }

    public virtual void Hit(int damage)
    {
        if(isImmortal)
        {
            Debug.Log("무적이여서 안 맞음");
            return;
        }
        currentHp -= damage;
        CheckDead();
    }

    protected virtual void CheckDead()
    {
        if (currentHp <= 0)
        {
            audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip, 1.0f);
            Die();
        }
    }

    public void ChangeIsImmortal(bool isImmortal, float immortalTime)
    {
        this.isImmortal = isImmortal;
        this.immortalTime = immortalTime;
    }

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
