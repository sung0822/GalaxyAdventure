using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal ItemData", menuName = "Item Data/Consumable Items/Heal ItemData", order = 1)]
public class HealItemData : ConsumableItemData
{
    public override ItemUsageType itemUsageType { get { return _itemUsageType; } }
    public override bool isUsing { get { return _isUsing; } set { _isUsing = value; } }
    public override UnitBase unitUser { get { return _unitUser; } set { _unitUser = value; } }
    [SerializeField] private UnitBase _unitUser;

    [SerializeField] private bool _isUsing = false;

    [SerializeField] ItemUsageType _itemUsageType;

    public override ItemBase CreateItem()
    {
        return new HealItem(this);
    }
}
