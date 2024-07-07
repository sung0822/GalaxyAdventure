using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    public UnitBase user { get ; set ; }

    protected  override int _id { get { return idValue; } set { idValue = value; } }


    private int idValue = 0;
    public override void Use()
    {
        user.currentHp = user.maxHp;
    }

    

}
