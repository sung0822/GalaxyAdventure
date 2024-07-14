using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItemBase : ItemBase
{
    public ConsumableItemData consumableItemData
    {
        get { return _consumableItemData; }
        set
        {
            SetData(value);
        }
    }

    private ConsumableItemData _consumableItemData;

    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _consumableItemData = (ConsumableItemData)itemData;
    }
}