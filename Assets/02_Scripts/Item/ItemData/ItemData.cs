using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public abstract class ItemData : ScriptableObject
{
    public int id { get { return _id; } }
    [SerializeField] protected int _id;
    public string itemName { get { return _itemName; } }
    [SerializeField] protected string _itemName;
    public abstract ItemType itemType { get; }
    public ItemUsageType itemUsageType { get { return _itemUsageType; } }
    [SerializeField] protected ItemUsageType _itemUsageType;
    public bool isUsing { get { return _isUsing; } set { _isUsing = value; } }
    [SerializeField] protected bool _isUsing;

    public UnitBase unitUser { get { return _unitUser; } set { _unitUser = value; } }
    protected UnitBase _unitUser;
    /// <summary> �����۽�ü�� �����մϴ�. ���ϴ� ������ Ÿ�Կ� �°� ĳ�����ؼ� ������</summary>
    public abstract ItemBase CreateItem();

    public virtual ItemData SetData(ItemData itemData)
    {
        this._id = itemData._id;
        this._itemName = itemData._itemName;
        this._itemUsageType = itemData._itemUsageType;
        this._isUsing = itemData._isUsing;

        return this;
    }


}
