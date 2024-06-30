using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Projectile
{
    
    public override void Shoot()
    {
        isShooting = true;
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
            transform.Translate(Vector3.forward * Time.deltaTime * _spd, Space.Self);
        }
    }
}
