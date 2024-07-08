using UnityEngine;

public abstract class WeaponBase : ItemBase, ITeamMember
{
    protected WeaponBase(UnitBase user, IAttackable attackableUser, WeaponSpace weaponSpace) : base(user)
    {
        this.attackableUser = attackableUser;
        this.weaponSpace = weaponSpace;
    }
    public override ItemType itemType { get { return _itemType; } }
    private ItemType _itemType = ItemType.Weapon;

    public abstract int power { get; set; }
    public IAttackable attackableUser { get; set; }
    public abstract float useCycle { get; set; }
    public abstract WeaponSpace weaponSpace { get; set; }
    public abstract TeamType teamType { get; set; }
    public abstract void SetUser(IAttackable unit);
    public abstract void SetStatus();
    
}