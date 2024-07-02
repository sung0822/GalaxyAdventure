using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalItem : ItemBase
{
    public Unit user { get; set; }
    protected override int _id { get { return idValue; } set { idValue = value; } }

    private int idValue = 1;

    public override void Use()
    {
        user.ChangeIsImmortal(true, 7.0f);
    }

}
