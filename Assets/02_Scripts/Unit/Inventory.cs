using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory
{
    /// <summary>
    /// ������ id�� �����մϴ�. List�� ���� ���� ���� �Ǳ���.
    /// </summary>
    private Dictionary<int, List<ItemBase>> items = new Dictionary<int, List<ItemBase>>();
    

    public void Add(ItemBase item)
    {
        if (items.ContainsKey(item.id))
        {
            items[item.id].Add(item);
        }
        else 
        {
            items.Add(item.id, new List<ItemBase>());
            items[item.id].Add(item);
        }
    }

    /// <summary>
    /// �� �������� ���� ��ȣ������. ��ȣ�� �ش� �����ۿ� ����.
    /// </summary>
    public ItemBase GetItem(int itemCode)
    {
        if (items.ContainsKey(itemCode))
        {
            return items[itemCode][0];
        }

        return null;
    }

    public int GetItemCount(int itemCode)
    {
        if (items.ContainsKey(itemCode))
        {
            return items[itemCode].Count;
        }

        return 0;
    }

    public int GetItemCount(ItemBase item)
    {
        int itemId = item.id;

        if(items.ContainsKey(itemId))
        {
            return items[itemId].Count;
        }

        return 0;
    }

    public bool CheckExist(ItemBase item)
    {
        if (items.ContainsKey(item.id))
        {
            return true;
        }

        return false;
    }
    public bool CheckExist(int itemId)
    {
        if (items.ContainsKey(itemId))
        {
            return true;
        }

        return false;
    }

}


public struct contatiner
{
    ItemBase item;

    int itemCount;
}
