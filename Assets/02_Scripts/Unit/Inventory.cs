using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory
{
    /// <summary>
    /// 아이템 id로 저장합니다. List에 실제 값이 담기게 되구요.
    /// </summary>
    private Dictionary<int, List<IItem>> items = new Dictionary<int, List<IItem>>();
    

    public void Add(IItem item)
    {
        if (items.ContainsKey(item.id))
        {
            items[item.id].Add(item);
            Debug.Log("아이템 추가됨");
        }
        else 
        {
            items.Add(item.id, new List<IItem>());
            items[item.id].Add(item);
            Debug.Log("아이템 추가됨");
        }
    }

    /// <summary>
    /// 각 아이템은 고유 번호가있음. 번호로 해당 아이템에 접근.
    /// </summary>
    public IItem GetItem(int itemCode)
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

    public int GetItemCount(IItem item)
    {
        int itemId = item.id;

        if(items.ContainsKey(itemId))
        {
            return items[itemId].Count;
        }

        return 0;
    }

    public bool CheckExist(IItem item)
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
    IItem item;

    int itemCount;
}
