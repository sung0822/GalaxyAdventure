using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : ICarryable, IItemUseable, IMovalble, IAttackable
{
    public int currentLevel { get; set; }
    public int maxLevel { get;}
    public float currentExp { get; set; }
    public float maxExp { get;}
    public float abilityGage { get; set; }
    public void LevelUp();
    public void LevelDown();

    public void GivePlayerExp(float exp);
    public Vector3 moveDir { get; set; }


}
