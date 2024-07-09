using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary> 장비 - 무기 아이템 </summary>
public abstract class WeaponItem : ItemBase
{
    public WeaponItemData weaponItemData { get { return _weaponItemData; } }

    protected WeaponItemData _weaponItemData;
    public WeaponItem(WeaponItemData data) : base(data) 
    {
        _weaponItemData = data;
    } 

}

