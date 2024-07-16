using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MachineGun : GunItemBase
{
    public MachineGunItemData machineGunItemData
    {
        get { return _machineGunItemData; }
        set
        {
            SetData(value);
        }
    }
    [SerializeField] protected MachineGunItemData _machineGunItemData;
    public override void Use()
    {
        
        if (machineGunItemData.shooted)
        {
            return;
        }
        CoroutineHelper.instance.RunCoroutine(StartShoot());
        if (machineGunItemData.isUsing)
        {
            return;
        }
        audioSource.Play();
        Debug.Log(machineGunItemData.name + "오디오플레이");
        machineGunItemData.isUsing = true;
    }
    protected override void Start()
    {
        base.Start();
        audioSource.loop = true;
    }

    protected override void Update()
    {
    }
    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _machineGunItemData = (MachineGunItemData)itemData;
    }

    public override void StopUse()
    {
        audioSource.Stop();
        machineGunItemData.isUsing = false;
    }
}
