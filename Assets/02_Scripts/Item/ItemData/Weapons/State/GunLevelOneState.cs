using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLevelOneState : IGunState
{
    public GunLevelOneState(GunItemData gunItemData)
    {
        this.gunItemData = gunItemData;
    }
    public GunItemData gunItemData { get; set; }

    public void Shoot()
    {
        GameObject projectileObject = GameObject.Instantiate<GameObject>
                                    (gunItemData.projectilePrefab,
                                    gunItemData.weaponSpaceTransform.transform.position,
                                    gunItemData.weaponSpaceTransform.transform.rotation);

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        projectile.power += gunItemData.power + gunItemData.attackableUser.power;
        projectile.spd = gunItemData.forceForProjectile;
        projectile.teamType = gunItemData.teamType;
        projectile.Shoot();
        
    }
}
