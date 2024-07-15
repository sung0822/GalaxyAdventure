using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSprayState : IGunState
{
    public GunSprayState(GunItemData gunItemData)
    {
        this.gunItemData = gunItemData;
    }
    public GunItemData gunItemData { get; set; }

    public void Shoot()
    {

        Quaternion rotation = gunItemData.weaponSpaceTransform.transform.rotation;
        for (int i = 0; i < 12; i++)
        {
            GameObject projectileObject = GameObject.Instantiate<GameObject>
                                    (gunItemData.projectilePrefab,
                                    gunItemData.weaponSpaceTransform.transform.position,
                                    rotation);
            rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + 30, rotation.eulerAngles.z);

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            projectile.power += gunItemData.power + gunItemData.attackableUser.power;
            projectile.spd = gunItemData.forceForProjectile;
            projectile.teamType = gunItemData.teamType;
            projectile.Shoot();
        }

    }
}
