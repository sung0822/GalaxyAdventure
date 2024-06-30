using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalItem : IItem
{
    public Unit user { get; set; }

    public int id { get {  return _itemCode; } }

    protected int _itemCode = 1;
    public void Use()
    {
        user.ChangeIsImmortal(true, 7.0f);
    }

}
