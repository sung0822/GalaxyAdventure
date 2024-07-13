using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : GunItemBase
{
    PistolItemData pistolItemData;
    public Pistol(PistolItemData gunItemdata) : base(gunItemdata)
    {
        pistolItemData = gunItemdata;
    }

    public override void Use()
    {
        base.Use();
    }
}
