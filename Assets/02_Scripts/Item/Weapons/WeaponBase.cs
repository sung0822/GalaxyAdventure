using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary> ��� - ���� ������ </summary>
public abstract class WeaponItem : ItemBase
{
    public WeaponItemData weaponItemData { get { return _weaponItemData; } }

    protected WeaponItemData _weaponItemData;
    public WeaponItem(WeaponItemData data) : base(data) 
    {
        _weaponItemData = data;
    } 

}

