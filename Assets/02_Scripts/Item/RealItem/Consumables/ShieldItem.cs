using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShieldItem : ConsumableItemBase
{
    ShieldItemData shieldItemData;
    public ShieldItem(ShieldItemData data) : base(data)
    {
        shieldItemData = data;
    }

    public override void Use()
    {
        shieldItemData.unitUser.SetImmortalDuring(true, shieldItemData.shieldTerm);
        GameObject barrier = GameObject.Instantiate<GameObject>(shieldItemData.barrierPrefab, shieldItemData.unitUser.transform);
        GameObject.Destroy(barrier, shieldItemData.shieldTerm);
    }
}
