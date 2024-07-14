using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "HealItemData", menuName = "Item Data/Consumable Items/HealItemData", order = 1)]
public class HealItemData : ConsumableItemData
{
    public float healAmountOfPercent { get { return _healAmountOfPercent; } }
    [SerializeField] float _healAmountOfPercent;
    public override ItemBase CreateItem()
    {
        GameObject gameObject = new GameObject(this.itemName);
        HealItem potion = gameObject.AddComponent<HealItem>();

        potion.consumableItemData = this;

        return potion;
    }
}
