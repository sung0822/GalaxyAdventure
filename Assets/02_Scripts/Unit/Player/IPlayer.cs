using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : IMovalble
{
    public float currentLevel { get; set; }
    public float currentExp { get; set; }
    public float maxExp { get; set; }

    public Inventory inventory { get;}

    public void ChangeSelcetedItem();
    public void UseItem();

    public Vector3 moveDir { get; set; }
    public void GivePlayerExp(float exp);


}
