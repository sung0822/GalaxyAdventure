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
    private Dictionary<int, List<ItemBase>> items = new Dictionary<int, List<ItemBase>>();

    private Dictionary<int, List<ConsumableItemBase>> consumableItems = new Dictionary<int, List<ConsumableItemBase>>();
    private Dictionary<int, List<WeaponBase>> weapons = new Dictionary<int, List<WeaponBase>>();

    public void Add(ItemBase item)
    {
        ItemType itemType = item.itemType;
        switch (itemType)
        {
            case ItemType.Consumable:
                AddToDictionary(consumableItems, (ConsumableItemBase)item);
                break;
         
            case ItemType.Weapon:
                AddToDictionary(weapons, (WeaponBase)item);
                break;
            
            default:
                break;
        }
    }
    private void AddToDictionary<T>(Dictionary<int, List<T>> dictionary, T item) where T : ItemBase
    {
        if (dictionary.ContainsKey(item.id))
        {
            dictionary[item.id].Add(item);
        }
        else
        {
            dictionary.Add(item.id, new List<T>());
            dictionary[item.id].Add(item);
        }
    }

    /// <summary>
    /// 각 아이템은 고유 번호가있음. 번호로 해당 아이템에 접근.
    /// </summary>
    public ItemBase GetItem(int id, ItemType itemType)
    {
        ItemBase item = null;
        switch (itemType)
        {
            case ItemType.Consumable:
                if (consumableItems.ContainsKey(id))
                    item = consumableItems[id][0];
                
                break;
            case ItemType.Weapon:
                if (weapons.ContainsKey(id))
                    item = weapons[id][0];

                break;
            default:
                break;
        }

        return item;
    }
    public int GetItemCount(int id, ItemType itemType)
    {
        int count = 0;
        switch (itemType)
        {
            case ItemType.Consumable:
                
                if (consumableItems.ContainsKey(id))
                    count = consumableItems[id].Count;
                break;
            case ItemType.Weapon:
                
                if (weapons.ContainsKey(id))
                    count = weapons[id].Count;
                break;
            default:
                break;
        }

        return count;
    }
    public void Remove(int id, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Consumable:
                
                if (!consumableItems.ContainsKey(id))
                {
                    return;
                } // 키를 포함하고있다면
                
                consumableItems[id].RemoveAt(0);
                
                if (consumableItems[id].Count <= 0)
                    consumableItems.Remove(id);
                break;
            case ItemType.Weapon:
                
                if (!weapons.ContainsKey(id))
                {
                    return;
                }
                
                weapons[id].RemoveAt(0);
                
                if (weapons[id].Count <= 0)
                    weapons.Remove(id);
                break;
            default:
                break;
        }
    }

    public bool CheckExist(int id, ItemType itemType)
    {
        bool isExist = false;

        switch (itemType)
        {
            case ItemType.Consumable:
                
                if (consumableItems.ContainsKey(id))
                    isExist = true;
                break;
            case ItemType.Weapon:
                
                if (weapons.ContainsKey(id))
                    isExist = true;
                break;
            default:
                break;
        }
        return isExist;
    }


}


public struct contatiner
{
    ItemBase item;

    int itemCount;
}
