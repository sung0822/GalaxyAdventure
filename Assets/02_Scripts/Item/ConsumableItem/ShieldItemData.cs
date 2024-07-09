using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield ItemData", menuName = "Item Data/Consumable Items/Shield ItemData", order = 1)]
public class ShieldItemData : ConsumableItemData
{
    public override ItemUsageType itemUsageType { get { return _itemUsageType; } }
    [SerializeField] ItemUsageType _itemUsageType;

}
