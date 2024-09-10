using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : GunItemBase
{
    public LaserGunItemData laserGunItemData
    {
        get { return _laserGunItemData; }
        set
        {
            SetData(value);
        }
    }
    [SerializeField] protected LaserGunItemData _laserGunItemData;
    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _laserGunItemData = (LaserGunItemData)itemData;
    }

    public override void Use()
    {
        if (gunItemData.shooted)
        {
            return;
        }

        StartCoroutine(StartShoot());
    }
    public override void StopUse()
    {
    }

    protected IEnumerator AudioStop(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.Stop();
    }

    protected override IEnumerator StartShoot()
    {
        GameObject go = Instantiate<GameObject>(laserGunItemData.projectilePrefab, laserGunItemData.weaponSpaceTransform.transform);
        HydroBeam hydroBeam = go.GetComponent<HydroBeam>();
        hydroBeam.power += laserGunItemData.attackableUser.power * 10;
        audioSource.clip = laserGunItemData.shootSound;
        audioSource.Play();
        Debug.Log("LaserGun 오디오플레이");
        StartCoroutine(AudioStop(5.0f));
        gunItemData.shooted = true;

        float time = 0;
        while (gunItemData.shooted)
        {
            time += Time.deltaTime;
            if (time >= gunItemData.useCycle)
            {
                Destroy(go);
                Debug.Log("쏘는게 끝남");
                gunItemData.shooted = false;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
