using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ConsumableItemBase
{
    public HealItem(ConsumableItemData data) : base(data)
    {
    }

    public override void Use()
    {
        consumableItemData.unitUser.currentHp = consumableItemData.unitUser.maxHp;
        if (consumableItemData.unitUser is Player)
        {
            UIManager.instance.CheckPlayerHp();
            Debug.Log("체크플레이어 호출");
        }
    }
}
