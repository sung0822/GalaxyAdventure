using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : ICarryable, IItemUseable, IMovalble
{
    public float currentLevel { get; set; }
    public float currentExp { get; set; }
    public float maxExp { get; set; }
    public void LevelUp();
    public void LevelDown();

    public void GivePlayerExp(float exp);
    public Vector3 moveDir { get; set; }


}
