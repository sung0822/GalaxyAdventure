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

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(isShooting)
        {
            Debug.Log("Shooting");
            projectileTransform.Translate(Vector3.forward * Time.deltaTime * 5);
        }
    }
}
