using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunItemBase : WeaponItemBase
{

    public GunItemData gunItemData { get { return _gunItemData; } }
    [SerializeField] private GunItemData _gunItemData;

    //public ShootableItemData shootableItemData { get { return _shootableItemData; } }
    //private ShootableItemData _shootableItemData;


    public GunItemBase(GunItemData gunItemdata) : base(gunItemdata)
    {
        _gunItemData = gunItemdata;
    }


    public override void Use()
    {
        gunItemData.gunState.OnShoot();
    }

    public void LevelUp()
    {
        gunItemData.level += 1;
    }
    public void LevelDown() 
    {
        gunItemData.level -= 1;
    }

}
