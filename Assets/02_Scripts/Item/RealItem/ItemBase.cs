using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IUseable
{
    void Use();
}

public abstract class ItemBase : IUseable
{

    public ItemData data { get { return _data; } }
    protected ItemData _data;
    public abstract void Use();
    public ItemBase(ItemData data)
    {
        this._data = data;
    }
    public string PrintItemName()
    {
        Debug.Log("itemName: " + data.itemName);
        return data.itemName;
    }
}

public abstract class ConsumableItemBase : ItemBase
{

    public ConsumableItemData consumableItemData { get { return _consumableItemData; } }
    
    private ConsumableItemData _consumableItemData;

    public ConsumableItemBase(ConsumableItemData data) : base(data)
    {
        _consumableItemData = data;
    }

}