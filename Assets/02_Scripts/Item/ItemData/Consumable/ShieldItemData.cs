using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield ItemData", menuName = "Item Data/Consumable Items/Shield ItemData", order = 1)]
public class ShieldItemData : ConsumableItemData
{
    public override bool isUsing { get { return _isUsing; } set { _isUsing = value; } }
    [SerializeField] private bool _isUsing = false;
    public override ItemUsageType itemUsageType { get { return _itemUsageType; } }

    public override UnitBase unitUser { get { return _unitUser; } set { _unitUser = value; } }
    [SerializeField] UnitBase _unitUser;

    [SerializeField] ItemUsageType _itemUsageType;
    public float shieldTerm { get {return _shieldTerm;} }
    [SerializeField] float _shieldTerm;

    public GameObject barrierPrefab { get { return _barrierPrefab; } }
    [SerializeField] GameObject _barrierPrefab;

    public override ItemBase CreateItem()
    {
        return new ShieldItem(this);
    }
}
