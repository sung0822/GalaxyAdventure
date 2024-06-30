using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : IItem
{
    public Unit user { get ; set ; }
    public int id { get { return _id; } }

    protected int _id = 2;
    public void Use()
    {
    }
}
