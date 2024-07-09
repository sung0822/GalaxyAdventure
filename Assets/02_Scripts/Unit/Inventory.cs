using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;


public class Inventory : MonoBehaviour
{
    /// <summary>
    /// ������ id�� �����մϴ�. List�� ���� ���� ���� �Ǳ���.
    /// </summary>
    private Dictionary<int, List<ItemData>> items = new Dictionary<int, List<ItemData>>();

    // �ִ� ���� �ѵ�(������ �迭 ũ��)
    [SerializeField, Range(8, 64)]
    private int _maxCapacity = 64;

    [SerializeField]
    private UIManager _inventoryUI; // ����� �κ��丮 UI

    /// <summary> ������ ��� </summary>
    [SerializeField]
    private void Awake()
    {
    }

    private void Start()
    {
    }

    public int Add(ItemData itemData, int amount = 1)
    {
        int id = itemData.id;
        if (items.ContainsKey(id))
        {
            Debug.Log("�̹� �����ϰ�����");
            items.Add(id, new List<ItemData>());
            return 1;
        }
        else
        {
            items.Add(id, new List<ItemData>());
            items[id].Add(itemData);
            Debug.Log(items[id].Count);
            return items[id].Count;
        }
    }
    public void Remove(int id, int removeCount = 1)
    {
        if (items.ContainsKey(id))
        {
            items[id].RemoveRange(0, removeCount);
        }
        
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

    /// <summary> �ش� ������ ������ ���� ���� </summary>
    public ItemData GetItemData(int id)
    {
        if (items.ContainsKey(id))
            return items[id][0];
        else
            return null;
    }

    
}