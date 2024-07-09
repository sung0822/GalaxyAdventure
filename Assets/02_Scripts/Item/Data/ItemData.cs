using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory System/Item Data", order = 1)]
public abstract class ItemData : ScriptableObject
{
    public int id => _id;

    [SerializeField] private int _id;
    public abstract ItemType itemType { get; }
    public abstract ItemUsageType itemUsageType { get; }

    /// <summary> 타입에 맞는 새로운 아이템 생성 </summary>
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory System/Item Data/WeaponItemData", order = 1)]
public abstract class WeaponItemData : ItemData, ITeamMember
{
    /// <summary> 공격력 </summary>
    public int power { get { return _power; } }
    [SerializeField] private int _power = 1;

    public int level { get { return _level; } }
    [SerializeField] private int _level;

    public WeaponSpace weaponSpace { get { return _weaponSpace; } }
    [SerializeField] private WeaponSpace _weaponSpace;
    
    public IAttackable attackableUser { get { return _attackableUser; } }
    [SerializeField] private IAttackable _attackableUser;
    public override ItemType itemType { get { return ItemType.Weapon; } }

    public abstract TeamType teamType { get; set; }

    public void SetStatus(int power, int level, WeaponSpace weaponSpace, IAttackable attackable, TeamType teamType)
    {
        _power = power;
        _level = level;
        _weaponSpace = weaponSpace;
        _attackableUser = attackable;
        _attackableUser = attackableUser;
    }

}

public abstract class ConsumableItemData : ItemData
{
    public override ItemType itemType { get { return ItemType.Consumable; } }
}

//public class ShootableItemData : ItemData
//{
//    public GameObject projectilePrefab;
//    public AudioClip audioClip;
//    public float useCycle;
//}