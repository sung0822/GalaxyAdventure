using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IUseable
{
    void Use();
}

public abstract class ItemBase : MonoBehaviour, IUseable
{

    public ItemData data { get { return _data; } set { _data = value; } }
    protected ItemData _data;
    public abstract void Use();
    public abstract void StopUse();
    /// <summary> ���丮 �޼��� </summary>
    public string PrintItemName()
    {
        Debug.Log("itemName: " + data.itemName);
        return data.itemName;
    }

    protected virtual void SetData(ItemData itemData)
    {
        _data = itemData;
    }
}
