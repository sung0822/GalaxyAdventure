using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Explosion : HitBox
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Projectile projectile = other.GetComponentInParent<Projectile>();
        if (projectile != null) 
        {
            Destroy(projectile.gameObject);
        }
    }
}
