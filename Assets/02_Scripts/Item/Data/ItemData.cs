using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public int id => _id;

    [SerializeField] private int _id;
    [SerializeField] private GameObject _dropItemPrefab; // 바닥에 떨어질 때 생성할 프리팹
    public abstract ItemType itemType { get; }
    public abstract ItemUsageType itemUsageType { get; }

    /// <summary> 타입에 맞는 새로운 아이템 생성 </summary>
}

[CreateAssetMenu(fileName = "ItemWeapon", menuName = "Inventory System/Item Data/Weaopn", order = 1)]
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

}

public class GunItemData : WeaponItemData, IShootable
{
    public override TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    private TeamType _teamType;

    public GameObject projectilePrefab { get { return _projectilePrefab; } set { _projectilePrefab = value; } }
    private GameObject _projectilePrefab;
    public AudioClip audioClip { get { return _audioClip; } set { _audioClip = value; } }
    private AudioClip _audioClip;
    public float useCycle { get { return _useCycle; } set { _useCycle = value; } }

    public override ItemUsageType itemUsageType { get { return ItemUsageType.ImmediatelyUse; } }

    public float _useCycle;

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}

//public class ShootableItemData : ItemData
//{
//    public GameObject projectilePrefab;
//    public AudioClip audioClip;
//    public float useCycle;
//}