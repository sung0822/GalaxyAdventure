using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEditor.Progress;
public abstract class ItemData : ScriptableObject
{
    public int id => _id;

    [SerializeField] private int _id;
    public string itemName { get { return _itemName; } }
    [SerializeField] string _itemName;
    public abstract ItemType itemType { get; }
    public abstract ItemUsageType itemUsageType { get; }
    public abstract bool isUsing { get; set; }

    public abstract UnitBase unitUser { get; set; }
    /// <summary> 아이템실체를 생성합니다. 원하는 아이템 타입에 맞게 캐스팅해서 쓰세요</summary>
    public abstract ItemBase CreateItem();

}
public abstract class WeaponItemData : ItemData, ITeamMember
{
    /// <summary> 공격력 </summary>
    public int power { get { return _power; } }
    [SerializeField] protected int _power = 1;

    public virtual int level { get { return _level; } set { _level = value; } }
    [SerializeField] protected int _level;

    public WeaponSpace weaponSpace { get { return _weaponSpace; } }
    [SerializeField] private WeaponSpace _weaponSpace;
    
    public IAttackable attackableUser { get { return _attackableUser; } }
    [SerializeField] private IAttackable _attackableUser;
    public override ItemType itemType { get { return ItemType.Weapon; } }
    public abstract TeamType teamType { get; set; }

    public virtual void SetStatus(int power, int level, WeaponSpace weaponSpace, IAttackable attackableUser, TeamType teamType, UnitBase unitUser)
    {
        _power = power;
        this.level = level;
        _weaponSpace = weaponSpace;
        _attackableUser = attackableUser;
        this.teamType = teamType;
        this.unitUser = unitUser;
    }


}

public abstract class ConsumableItemData : ItemData
{
    public override ItemType itemType { get { return ItemType.Consumable; } }
    public virtual void SetStatus(UnitBase unitUser)
    {
        this.unitUser = unitUser;
    }
}

//public class ShootableItemData : ItemData
//{
//    public GameObject projectilePrefab;
//    public AudioClip audioClip;
//    public float useCycle;
//}