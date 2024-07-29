using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;


public class Inventory : MonoBehaviour
{
    /// <summary>
    /// 아이템 id로 저장합니다. List에 실제 값이 담기게 되구요.
    /// </summary>
    [SerializeField] private Dictionary<int, List<ItemData>> items = new Dictionary<int, List<ItemData>>();

    [SerializeField]
    private UIManager _inventoryUI; // 연결된 인벤토리 UI

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

    /// <summary> 아이템 데이터 정보참조 리턴. </summary>
    public ItemData GetItemData(int id)
    {
        if (items.ContainsKey(id))
            return items[id][0];
        else
            return null;
    }

    
}