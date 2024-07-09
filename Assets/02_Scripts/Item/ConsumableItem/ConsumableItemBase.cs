using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItemBase : ItemBase
{
    public override ItemType itemType { get { return _itemType; } }
    private ItemType _itemType = ItemType.Consumable;

    public ConsumableItemBase(UnitBase user) : base(user)
    {

    }
    public ConsumableItemBase()
    {
    }

}
