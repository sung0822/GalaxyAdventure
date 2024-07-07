using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, ITeamMember
{
    public int power { get => _power; set => _power = value; }
    public float spd { get => _spd; set => _spd = value; }

    protected int _power = 0;
    protected float _spd = 0;
    
    protected bool isShooting;

    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }

    protected TeamType _teamType;

    protected bool isDestroied;


    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {

    }

    protected void OnTriggerEnter(Collider other)
    {
        if (isDestroied)
        {
            return;
        }
        if (other.transform.GetComponentInParent<UnitBase>() != null)
        {
            UnitBase unit = other.transform.GetComponentInParent<UnitBase>();

            if (unit.teamType != _teamType)
            {
                unit.Hit(_power);
                
                Destroy(this.gameObject);

                return;
            }
            else
            {
            }

        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WALL"))
        {
            Destroy(this.gameObject);
            return;
        }
        else if (collision.transform.GetComponentInParent<UnitBase>() != null)
        {
            if (collision.transform.GetComponent<UnitBase>().teamType != teamType)
            {
                collision.transform.GetComponent<UnitBase>().Hit(_power);
                Destroy(this.gameObject);
                return;
            }
            else
            {
            }
        }

        
    }

    public abstract void Shoot();



}
