using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    /// <summary>
    /// ������ id�� �����մϴ�. List�� ���� ���� ���� �Ǳ���.
    /// </summary>
    [SerializeField] private Dictionary<int, List<ItemData>> items = new Dictionary<int, List<ItemData>>();

    [SerializeField]
    private UIManager _inventoryUI; // ����� �κ��丮 UI

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public ItemData Add(ItemData itemData, int amount = 1)
    {
        int id = itemData.id;
        if (items.ContainsKey(id))
        {
            items[id].Add(itemData);
            return items[id][0]; 
        }
        else
        {
            items.Add(id, new List<ItemData>());
            items[id].Add(itemData);
            return items[id][0];
        }
    }
    public int Remove(int id, int removeCount = 1)
    {
        if (items.ContainsKey(id))
        {
            items[id].RemoveRange(0, removeCount);
            if (items[id].Count <= 0)
            {
                items.Remove(id);
                return 0;
            }
            return items[id].Count;
        }
        return 0;
    }

    public int GetItemCount(int id)
    {
        if (items.ContainsKey(id))
        {
            return items[id].Count;
        }
        else
        {
            return 0;
        }
    }

    public bool CheckExist(int id)
    {
        if (items.ContainsKey(id))
            return true;
        else
            return false;
    }

    /// <summary> ������ ������ �������� ����. </summary>
    public ItemData GetItemData(int id)
    {
        if (items.ContainsKey(id))
            return items[id][0];
        else
            return null;
    }

    
}