using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PistolItemData", menuName = "Item Data/Gun ItemData/PistolItemData", order = 1)]
public class PistolItemData : GunItemData
{
    public override ItemBase CreateItem()
    {
        return new Pistol(this);
    }
}
