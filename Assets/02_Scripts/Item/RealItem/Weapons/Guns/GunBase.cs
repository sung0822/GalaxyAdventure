using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : WeaponItemBase
{

    public GunItemData gunItemData { get { return _gunItemData; } }
    [SerializeField] private GunItemData _gunItemData;

    //public ShootableItemData shootableItemData { get { return _shootableItemData; } }
    //private ShootableItemData _shootableItemData;

    public GunBase(GunItemData gunItemdata) : base(gunItemdata)
    {
        _gunItemData = gunItemdata;
    }


    public override void Use()
    {
        if (_gunItemData.isUsing)
        {
            return;
        }
        CoroutineHelper.instance.RunCoroutine(StartShoot());
    }

    public virtual void Shoot()
    {
        GameObject projectileObject = GameObject.Instantiate<GameObject>
            (gunItemData.projectilePrefab,
            weaponItemData.weaponSpace.transform.position,
            weaponItemData.weaponSpace.transform.rotation);

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        projectile.power += _gunItemData.attackableUser.power;
        projectile.spd = 10;
        projectile.teamType = _gunItemData.teamType;
        projectile.Shoot();
    }

    IEnumerator StartShoot()
    {
        Shoot();
        gunItemData.isUsing = true;
        float time = 0;
        while (gunItemData.isUsing)
        {
            time += Time.deltaTime;
            if (time >= gunItemData.useCycle)
            {
                gunItemData.isUsing = false;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
