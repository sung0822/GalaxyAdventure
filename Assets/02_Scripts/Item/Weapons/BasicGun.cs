using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BasicGun : WeaponItem
{

    public GunItemData gunItemData { get { return _gunItemData; } }
    private GunItemData _gunItemData;

    //public ShootableItemData shootableItemData { get { return _shootableItemData; } }
    //private ShootableItemData _shootableItemData;

    public BasicGun(GunItemData gunItemdata) : base(gunItemdata)
    {
        _gunItemData = gunItemdata;
    }
    

    public override void Use()
    {
        Shoot();
    }

    public void Shoot()
    {
        GameObject projectileObject = GameObject.Instantiate<GameObject>
            (gunItemData.projectilePrefab, 
            weaponItemData.weaponSpace.transform.position, 
            weaponItemData.weaponSpace.transform.rotation);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Shoot();

    }

}
