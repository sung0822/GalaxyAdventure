using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLevelOneState : IGunState
{
    public GunLevelOneState(GunItemData gunItemData)
    {
        this.gunItemData = gunItemData;
        gunItemData.isUsing = false;
    }
    public GunItemData gunItemData { get; set; }

    public void OnShoot()
    {
        if (gunItemData.isUsing)
        {
            return;
        }
        CoroutineHelper.instance.RunCoroutine(StartShoot());
    }
    public void Shoot()
    {
        GameObject projectileObject = GameObject.Instantiate<GameObject>
                                    (gunItemData.projectilePrefab,
                                    gunItemData.weaponSpace.transform.position,
                                    gunItemData.weaponSpace.transform.rotation);

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        projectile.power += gunItemData.power + gunItemData.attackableUser.power;
        projectile.spd = gunItemData.forceForProjectile;
        projectile.teamType = gunItemData.teamType;
        
        AudioSource.PlayClipAtPoint(gunItemData.shootSound, gunItemData.weaponSpace.transform.position);
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
