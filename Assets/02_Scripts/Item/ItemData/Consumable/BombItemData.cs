using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BombItemData", menuName = "Item Data/Consumable Items/BombItemData", order = 1)]
public class BombItemData : ConsumableItemData
{
    public override bool isUsing { get { return _isUsing; } set { _isUsing = value; } }
    [SerializeField] private bool _isUsing = false;
    public override ItemUsageType itemUsageType { get { return _itemUsageType; } }
    [SerializeField] ItemUsageType _itemUsageType;

    public override UnitBase unitUser { get { return _unitUser; } set { _unitUser = value; } }
    [SerializeField] private UnitBase _unitUser;


    public override ItemBase CreateItem()
    {
        return new BombItem(this);
    }
}
