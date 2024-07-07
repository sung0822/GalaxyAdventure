using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, ITeamMember
{
    public IAttackable user { get; set; }
    public int power { get; set; }
    public abstract float useCycle { get; set; }
    public abstract TeamType teamType { get; set; }

    public abstract void SetUser(IAttackable unit);
    public abstract void Use();
}