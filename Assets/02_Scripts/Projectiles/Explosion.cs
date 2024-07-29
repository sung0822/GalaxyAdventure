using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Explosion : HitBox
{
    protected override void OnTriggerEnter(Collider other)
    {
        UnitBase unit = other.transform.GetComponentInParent<UnitBase>();
        if (unit != null)
        {
            if (unit.tag == "BOSS")
            {
                return;
            }
            if (unit.teamType != _teamType)
            {
                Vector3 closetPoint = other.ClosestPoint(transform.position);
                unit.Hit(power, closetPoint);

                return;
            }
        }
        Projectile projectile = other.GetComponentInParent<Projectile>();
        if (projectile != null) 
        {
            Destroy(projectile.gameObject);
        }

    }
}
