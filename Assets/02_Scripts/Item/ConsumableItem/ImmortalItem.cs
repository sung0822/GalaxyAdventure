using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalItem : ConsumableItemBase
{
    public override UnitBase user { get { return _user; } set { _user = value; } }
    private UnitBase _user = null;

    public override int id { get { return _id; } }

    public override ItemUsageType usageType => throw new System.NotImplementedException();

    private int _id = 1;

    public ImmortalItem(UnitBase user) : base(user)
    {
    }
    public ImmortalItem()
    {

    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
