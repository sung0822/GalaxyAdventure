using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SniperItemData", menuName = "Item Data/Gun ItemData/SniperItemData", order = 1)]
public class MachineGunItemData : GunItemData
{
    public override ItemBase CreateItem()
    {
        return new MachineGun(this);
    }
}
