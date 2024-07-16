using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShieldItem : ConsumableItemBase
{
    public ShieldItemData shieldItemData
    {
        get { return _shieldItemData; }
        set
        {
            SetData(value);
        }
    }

    [SerializeField] ShieldItemData _shieldItemData;
    public override void Use()
    {
        shieldItemData.unitUser.SetImmortalDuring(true, shieldItemData.shieldTerm);
        transform.parent = shieldItemData.unitUser.transform;
        transform.position = shieldItemData.unitUser.transform.position;
        GameObject barrier = GameObject.Instantiate<GameObject>(shieldItemData.barrierPrefab, shieldItemData.unitUser.transform);

        Debug.Log("shieldTerm: " + shieldItemData.shieldTerm);
        GameObject.Destroy(barrier, shieldItemData.shieldTerm);
        GameObject.Destroy(this.gameObject, shieldItemData.shieldTerm);

    }
    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _shieldItemData = (ShieldItemData)itemData;
    }

    public override void StopUse()
    {
    }
}
