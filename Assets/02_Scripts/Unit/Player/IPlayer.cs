using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : ICarryable, IItemUseable, IMovalble, IAttackable
{
    public int currentLevel { get; set; }
    public PlayerLevelUpData playerLevelUpData { get; set;}
    public float currentExp { get; }
    public float currentAbilityGage { get; set; }
    public void LevelUp();
    public void LevelDown();
    public void GivePlayerExp(float exp);
    public void GivePlayerAbilityGage(float abilityGage);
    public Vector3 moveDir { get; set; }


}
