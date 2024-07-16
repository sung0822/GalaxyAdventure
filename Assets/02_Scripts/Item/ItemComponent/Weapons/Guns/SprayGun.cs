using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayGun : GunItemBase
{
    public SprayGunItemData sprayGunItemData
    {
        get { return _sprayGunItemData; }
        set
        {
            SetData(value);
            _sprayGunItemData.SetGunState(new GunSprayState(_sprayGunItemData));
        }
    }
    [SerializeField] protected SprayGunItemData _sprayGunItemData;


    public override void Use()
    {
        if (sprayGunItemData.shooted)
        {
            return;
        }
        CoroutineHelper.instance.RunCoroutine(StartShoot());
        if (sprayGunItemData.isUsing)
        {
            return;
        }
        audioSource.Play();
        Debug.Log("오디오플레이");
        sprayGunItemData.isUsing = true;
    }


    public override void StopUse()
    {
    }
    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _sprayGunItemData = (SprayGunItemData)itemData;
    }

}
