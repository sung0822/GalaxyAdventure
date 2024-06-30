using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : IItem
{
    public Unit user { get ; set ; }

    public int id { get { return _itemCode; } }

    protected int _itemCode = 0;
    public void Use()
    {
        user.currentHp = user.maxHp;
    }

    

}
