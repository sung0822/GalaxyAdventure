using UnityEngine;

public abstract class WeaponBase : ItemBase, ITeamMember
{
    public abstract int power { get; set; }
    public IAttackable attackableUser { get; set; }
    public abstract float useCycle { get; set; }
    public abstract TeamType teamType { get; set; }
    public abstract WeaponSpace weaponSpace { get; set; }
    public abstract void SetUser(IAttackable unit);
    public abstract void SetStatus();
    
}