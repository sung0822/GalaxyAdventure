using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ConsumableItemBase
{
    public HealItemData healItemData
    {
        get { return _healItemData; }
        set
        {
            SetData(value);
        }
    }

    [SerializeField] HealItemData _healItemData;

    public override void Use()
    {

        float healAmount = healItemData.unitUser.currentHp + (float)(healItemData.unitUser.maxHp) * healItemData.healAmountOfPercent;

        healItemData.unitUser.currentHp = healItemData.unitUser.currentHp + (int)healAmount;

        if (consumableItemData.unitUser is Player)
        {
            UIManager.instance.CheckPlayerHp();
        }
    }

    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _healItemData = (HealItemData)itemData;
    }

    public override void StopUse()
    {
    }
}
