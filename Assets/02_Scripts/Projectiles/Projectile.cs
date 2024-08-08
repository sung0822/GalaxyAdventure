using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, ITeamMember
{
    public abstract int power { get; set; }
    public float spd { get { return _spd; } set { _spd = value; } }
    protected float _spd = 0;

    protected bool isShooting;
    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }

    protected TeamType _teamType;

    protected bool isDestroied;


    protected virtual void Start()
    {
        Destroy(gameObject, 15.0f);
    }

    protected virtual void Update()
    {
        if (isShooting)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _spd, Space.Self);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (isDestroied)
        {
            return;
        }
        if (other.tag == "HYDRO_BEAM")
        {
            return;
        }

        if (other.transform.GetComponentInParent<UnitBase>() != null)
        {
            UnitBase unit = other.transform.GetComponentInParent<UnitBase>();
            if (unit.teamType != _teamType)
            {
                Vector3 closetPoint = other.ClosestPoint(transform.position);
                unit.Hit(power, closetPoint);

                isDestroied = true;
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
                collision.transform.GetComponent<UnitBase>().Hit(power);
                Destroy(this.gameObject);
                return;
            }
        }

        
    }

    public virtual void Shoot()
    {
        isShooting = true;
    }



}
