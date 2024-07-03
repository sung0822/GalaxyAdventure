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

    public TeamType Team { get { return _myTeam; } set { _myTeam = value; } }
    protected TeamType _myTeam;

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
        if (other.transform.GetComponentInParent<Unit>()?.gameObject.layer == LayerMask.NameToLayer("UNIT"))
        {
            Unit unit = other.transform.GetComponentInParent<Unit>();

            if (unit.Team != _myTeam)
            {
                unit.Hit(_power);
                GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, this.transform.position, Quaternion.Euler(0, 0, 0));

                Destroy(particle, 0.7f);
                
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
        else if (collision.transform.GetComponentInParent<Unit>()?.gameObject.layer == LayerMask.NameToLayer("UNIT"))
        {
            Debug.Log("À¯´Ö ºÎµúÈû!!");
            if (collision.transform.GetComponent<Unit>().Team != Team)
            {
                Debug.Log("Àû±º ºÎµúÈû!!");
                collision.transform.GetComponent<Unit>().Hit(_power);
                Destroy(this.gameObject);
                return;
            }
            else
            {
                Debug.Log("¾Æ±º ºÎµúÈû!!");
            }
        }

        
    }

    public abstract void Shoot();



}
