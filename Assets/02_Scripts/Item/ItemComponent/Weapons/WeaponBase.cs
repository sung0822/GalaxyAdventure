using TMPro;
using UnityEngine;

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

