using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Unit
{

    List<IWeapon> weapons = new List<IWeapon>();

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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
