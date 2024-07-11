using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SniperitemData", menuName = "Item Data/Gun ItemData/SniperitemData", order = 1)]
public class SniperitemData : GunItemData
{
    public override ItemBase CreateItem()
    {
        return new Sniper(this);
    }
}
