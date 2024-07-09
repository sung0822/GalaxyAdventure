using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Projectile
{
    public override int id { get { return _id; }  }
    private int _id = 0;
    protected override GameObject projectileSelfPrefab { get; set; }

    public override void Shoot()
    {
        isShooting = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
    }
}
