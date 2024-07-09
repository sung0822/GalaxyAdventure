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
    private Dictionary<int, List<ItemData>> items = new Dictionary<int, List<ItemData>>();

    // 최대 수용 한도(아이템 배열 크기)
    [SerializeField, Range(8, 64)]
    private int _maxCapacity = 64;

    [SerializeField]
    private UIManager _inventoryUI; // 연결된 인벤토리 UI

    /// <summary> 아이템 목록 </summary>
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
            Debug.Log("이미 포함하고있음");
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

    /// <summary> 해당 슬롯의 아이템 정보 리턴 </summary>
    public ItemData GetItemData(int id)
    {
        if (items.ContainsKey(id))
            return items[id][0];
        else
            return null;
    }

    
}