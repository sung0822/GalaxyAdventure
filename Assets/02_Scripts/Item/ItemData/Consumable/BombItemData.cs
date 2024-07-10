using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomb ItemData", menuName = "Item Data/Consumable Items/Bomb ItemData", order = 1)]
public class BombItemData : ConsumableItemData
{
    public override bool isUsing { get { return _isUsing; } set { _isUsing = value; } }
    [SerializeField] private bool _isUsing = false;
    public override ItemUsageType itemUsageType { get { return _itemUsageType; } }

    public override UnitBase unitUser { get { return _unitUser; } set { _unitUser = value; } }
    [SerializeField] private UnitBase _unitUser;

    [SerializeField] ItemUsageType _itemUsageType;

    public override ItemBase CreateItem()
    {
        return new BombItem(this);
    }
}
