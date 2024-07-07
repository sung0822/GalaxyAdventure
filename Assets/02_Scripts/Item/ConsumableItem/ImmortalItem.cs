using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalItem : ItemBase
{
    public UnitBase user { get; set; }
    protected override int _id { get { return idValue; } set { idValue = value; } }

    private int idValue = 1;

    public override void Use()
    {
        user.SetImmortal(true, 7.0f);
    }

}
