using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int power = 0;
    protected Transform bulletTransform;
    protected Rigidbody bulletRigidbody;
    
    protected bool isShooting;
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hit(power);
            Destroy(this);
        }
    }

    public abstract void Shoot();
    

}
