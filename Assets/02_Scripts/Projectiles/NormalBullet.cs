using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Projectile
{
    public override int power { get { return _power; } set { _power = value; } }
    protected int _power = 15;
    public override void Shoot()
    {
        base.Shoot();
    }

    protected override void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
