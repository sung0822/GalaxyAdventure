using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary> 장비 - 무기 아이템 </summary>
public abstract class WeaponItemBase : ItemBase
{
    public WeaponItemData weaponItemData
    {
        get { return _weaponItemData; }
        set
        {
            SetData(value);
        }
    }

    protected WeaponItemData _weaponItemData;

    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _weaponItemData = (WeaponItemData)itemData;
    }

}

