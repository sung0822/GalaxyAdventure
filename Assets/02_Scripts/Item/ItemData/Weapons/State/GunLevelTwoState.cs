using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLevelTwoState : IGunState
{
    public GunLevelTwoState(GunItemData gunItemData)
    {
        this.gunItemData = gunItemData;
    }
    public GunItemData gunItemData { get; set; }

    public void ChangeGunState(IGunState gunState)
    {
    }

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

        Transform shootSpace = gunItemData.weaponSpace.transform;

        GameObject projectileObject = GameObject.Instantiate<GameObject>
                                    (gunItemData.projectilePrefab,
                                    shootSpace.position + shootSpace.right * 0.5f,
                                    shootSpace.rotation);

        GameObject projectileObject2 = GameObject.Instantiate<GameObject>
                                    (gunItemData.projectilePrefab,
                                    shootSpace.position + shootSpace.right * -0.5f,
                                    shootSpace.rotation);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        Projectile projectile2 = projectileObject2.GetComponent<Projectile>();



        projectile.power += gunItemData.attackableUser.power;
        projectile.spd = gunItemData.forceForProjectile;
        projectile.teamType = gunItemData.teamType;
        
        projectile2.power += gunItemData.attackableUser.power;
        projectile2.spd = gunItemData.forceForProjectile;
        projectile2.teamType = gunItemData.teamType;


        //AudioSource.PlayClipAtPoint(audioClip, transform.position);
        projectile.Shoot();
        projectile2.Shoot();
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
