using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomb ItemData", menuName = "Item Data/Consumable Items/Bomb ItemData", order = 1)]
public class BombItemData : ConsumableItemData
{
    public override ItemUsageType itemUsageType { get { return _itemUsageType; } }
    [SerializeField] ItemUsageType _itemUsageType;

}
