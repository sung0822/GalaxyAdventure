using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MachineGunItemData", menuName = "Item Data/Gun ItemData/MachineGunItemData", order = 1)]
public class MachineGunItemData : GunItemData
{
    public override ItemBase CreateItem()
    {
        return new MachineGun(this);
    }
}
