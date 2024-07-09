using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal ItemData", menuName = "Item Data/Consumable Items/Heal ItemData", order = 1)]
public class HealItemData : ConsumableItemData
{
    public override ItemUsageType itemUsageType { get { return _itemUsageType; } }
    [SerializeField] ItemUsageType _itemUsageType;

}
