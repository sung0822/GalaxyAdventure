using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public abstract bool isAttacking { get; set; }

    protected virtual void Start()
    {
        _power = 10;
        maxHp = 100;
        currentHp = 100;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isImmortal)
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
            return;
        }
        currentHp -= damage;
    }

    public virtual void CheckDead()
    {
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void ChangeIsImmortal(bool isImmortal)
    {
        this.isImmortal = isImmortal;
        immortalTime = float.MaxValue;
    }

    protected virtual void Die()
    {
        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.unitExplodingParticle, transform.position, transform.rotation);

        particle.transform.localScale = this.transform.localScale * 0.1f;
        Destroy(particle, 1.5f);

        Destroy(gameObject);
    }

    protected virtual void LateUpdate()
    {
    }




}
