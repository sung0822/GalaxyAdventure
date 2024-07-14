using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPiercingBullet : Projectile
{
    public override int power { get { return _power; } set { _power = value; } }
    protected int _power = 5;
    public override void Shoot()
    {
        base.Shoot();
    }

    protected override void Start()
    {
    }

    protected override void Update()
    {
        if (isShooting)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _spd, Space.Self);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (isDestroied)
        {
            return;
        }

        if (other.tag == "HYDRO_BEAM")
        {
            return;
        }

        if (other.transform.GetComponentInParent<UnitBase>() != null)
        {
            UnitBase unit = other.transform.GetComponentInParent<UnitBase>();
            if (unit.teamType != _teamType)
            {
                Vector3 closetPoint = other.ClosestPoint(transform.position);
                unit.Hit(power, closetPoint);

                return;
            }

        }
    }
}
