using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    IWeapon weapon = new BasicGun();
    protected override void Start()
    {
        base.Start();

        patterns.Add(new Pattern1());
        patterns[0].SetTarget(this.transform);

    }

    protected override void Update()
    {
        base.Update();
        patterns[0].Execute();
        //weapon.Use();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
