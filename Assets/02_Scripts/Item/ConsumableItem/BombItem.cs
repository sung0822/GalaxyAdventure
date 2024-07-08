using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : ConsumableItemBase
{
    public override UnitBase user { get { return _user; } set { _user = value; } }
    private UnitBase _user = null;
    public override int id { get; }
    public override ItemType itemType { get { return _itemType; } }

    public override ItemUsageType usageType { get { return _usageType; } }
    private ItemUsageType _usageType = ItemUsageType.NotImmediatelyUse;

    private ItemType _itemType = ItemType.Consumable;

    public BombItem(UnitBase user) : base(user)
    {
    }

    public override void Use()
    {

    }
}
