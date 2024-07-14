using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MachineGunItemData", menuName = "Item Data/Gun ItemData/MachineGunItemData", order = 1)]
public class MachineGunItemData : GunItemData
{
    public override ItemBase CreateItem()
    {
        GameObject gameObject = new GameObject(this.itemName);
        MachineGun machineGun = gameObject.AddComponent<MachineGun>();

        machineGun.gunItemData = this;


        return machineGun;
    }
}
