using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : ItemBase
{
    public Unit user { get ; set ; }
    protected override int _id { get { return idValue; } set { idValue = value; } }
    private int idValue = 2;

    public override void Use()
    {
    }
}
