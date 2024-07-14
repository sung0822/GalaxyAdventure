using UnityEngine;

public abstract class WeaponItemData : ItemData, ITeamMember
{
    /// <summary> °ø°Ý·Â </summary>
    public int power { get { return _power; } set { _power = value; } }
    [SerializeField] protected int _power = 1;

    public virtual int level { get { return _level; } set { _level = value; } }
    [SerializeField] protected int _level;

    public Transform weaponSpaceTransform { get { return _weaponSpaceTransform; } set { _weaponSpaceTransform = value; } }
    [SerializeField] private Transform _weaponSpaceTransform;

    public IAttackable attackableUser { get { return _attackableUser; } set { _attackableUser = value; } }
    [SerializeField] private IAttackable _attackableUser;
    public override ItemType itemType { get { return ItemType.Weapon; } }
    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    [SerializeField] protected TeamType _teamType;

    public override ItemData SetData(ItemData itemData)
    {

        if (itemData is WeaponItemData)
        {
            WeaponItemData weaponItemData = (WeaponItemData)itemData;
            base.SetData(weaponItemData);
            this._level = weaponItemData.level;
            this._weaponSpaceTransform = weaponItemData.weaponSpaceTransform;
            this._attackableUser = weaponItemData.attackableUser;
            this._teamType = weaponItemData.teamType;
            return this;

        }
        else
        {
            return null;
        }
    }


}
