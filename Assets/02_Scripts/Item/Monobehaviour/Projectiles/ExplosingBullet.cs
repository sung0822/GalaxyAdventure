using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosingBullet : Projectile
{
    public override int power { get { return _power; } set { _power = value; } }
    protected int _power = 0;

    GameObject explosion;
    public override void Shoot()
    {
        base.Shoot();
    }

    protected override void Start()
    {
    }

    protected override void Update()
    {
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (isDestroied)
        {
            return;
        }
        if (other.transform.GetComponentInParent<UnitBase>() != null)
        {
            UnitBase unit = other.transform.GetComponentInParent<UnitBase>();
            if (unit.teamType != _teamType)
            {
                Vector3 closetPoint = other.ClosestPoint(transform.position);
                isDestroied = true;
                Destroy(this.gameObject);

                return;
            }
            else
            {
            }

        }
    }
}
