using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, ITeamMember
{
    public int power { get => _power; set => _power = value; }
    public float spd { get => _spd; set => _spd = value; }

    protected int _power = 0;
    protected float _spd = 0;

    protected Transform projectileTransform;
    protected Rigidbody bulletRigidbody;
    
    protected bool isShooting;

    public TeamType Team { get; }
    TeamType _team = TeamType.Ally;


    protected virtual void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        projectileTransform = GetComponent<Transform>();
    }

    void Update()
    {

    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ENEMY")
        {
            collision.gameObject.GetComponent<Enemy>().Hit(power);
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "WALL") 
        {
            Destroy(gameObject);
        }
    }

    public abstract void Shoot();
    

}
