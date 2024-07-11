using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : ConsumableItemBase
{
    BombItemData bombItemData;
    public BombItem(BombItemData bombItemData) : base(bombItemData)
    {
        this.bombItemData = bombItemData;
    }

    public override void Use()
    {
        Bomber bomber = GameObject.Instantiate<GameObject>(this.bombItemData.bomber).GetComponent<Bomber>();
        bomber.teamType = TeamType.ALLY;
    }
}
