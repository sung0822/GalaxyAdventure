using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : GunItemBase
{
    public PistolItemData pistolItemData 
    { 
        get { return _pistolItemData; } 
        set 
        {
            SetData(value);
        }
    }
    [SerializeField] protected PistolItemData _pistolItemData;

    public override void Use()
    {
        base.Use();
    }

    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _pistolItemData = (PistolItemData)itemData;
    }

    public override void StopUse()
    {
    }
}
