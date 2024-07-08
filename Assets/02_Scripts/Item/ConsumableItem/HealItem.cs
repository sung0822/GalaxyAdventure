using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{


    private int idValue = 0;

    public override UnitBase user { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override int id => throw new System.NotImplementedException();

    public override void Use()
    {
        user.currentHp = user.maxHp;
    }

    

}
