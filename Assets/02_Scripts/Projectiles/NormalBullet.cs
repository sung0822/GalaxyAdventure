using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Projectile
{
    public override void Shoot()
    {
        isShooting = true;
        //bulletRigidbody.AddForce(Vector3.forward);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isShooting)
        {
            bulletTransform.Translate(Vector3.forward);
        }
    }
}
