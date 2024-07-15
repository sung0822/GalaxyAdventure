using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : ConsumableItemBase
{
    public BombItemData bombItemData
    {
        get { return _bombItemData; }
        set
        {
            SetData(value);
        }
    }

    [SerializeField] protected BombItemData _bombItemData;


    public override void Use()
    {
        Bomber bomber = GameObject.Instantiate(this.bombItemData.bomber).GetComponent<Bomber>();
        Debug.Log("아아아아");
        bomber.name = "아아아아";
        bomber.teamType = TeamType.ALLY;
    }
    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _bombItemData = (BombItemData)itemData;
    }

    public override void StopUse()
    {
    }
}
