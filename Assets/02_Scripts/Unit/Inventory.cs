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
    private Dictionary<int, List<IItem>> items = new Dictionary<int, List<IItem>>();
    

    public void Add(IItem item)
    {
        if (items.ContainsKey(item.id))
        {
            items[item.id].Add(item);
            Debug.Log("������ �߰���");
        }
        else 
        {
            items.Add(item.id, new List<IItem>());
            items[item.id].Add(item);
            Debug.Log("������ �߰���");
        }
    }

    /// <summary>
    /// �� �������� ���� ��ȣ������. ��ȣ�� �ش� �����ۿ� ����.
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
