using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory
{
    /// <summary>
    /// 아이템 id로 저장합니다. List에 실제 값이 담기게 되구요.
    /// </summary>
    private Dictionary<int, List<IItemAttribute>> items = new Dictionary<int, List<IItemAttribute>>();

    private Dictionary<int, List<ConsumableItemBase>> consumableItems = new Dictionary<int, List<ConsumableItemBase>>();
    private Dictionary<int, List<WeaponBase>> weapons = new Dictionary<int, List<WeaponBase>>();
    private Dictionary<int, List<Projectile>> projectiles = new Dictionary<int, List<Projectile>>();

    public void Add(IItemAttribute item)
    {
        switch (item.itemType)
        {
            case ItemType.Consumable:
                AddToDictionary(consumableItems, (ConsumableItemBase)item);
                
                break;
            case ItemType.Weapon:
                AddToDictionary(weapons, (WeaponBase)item);
                
                break;
                case ItemType.Projectile:
                
                AddToDictionary(projectiles, (Projectile)item);
                break;
            default:
                break;
        }
    }
    public void Add(IItemAttribute[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Add(items[i]);
        }
    }
    private void AddToDictionary<T>(Dictionary<int, List<T>> dictionary, T item) where T : IItemAttribute
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
    public T GetItem<T>(int id, ItemType itemType) where T : IItemAttribute
    {
        IItemAttribute item = null;

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

        return (T)item;
    }
    public T[] GetItems<T>(int id, ItemType itemType, int count) where T : IItemAttribute
    {
        switch (itemType)
        {
            case ItemType.Consumable:
                if (consumableItems.ContainsKey(id))
                {
                    ConsumableItemBase[] array = consumableItems[id].GetRange(0, count).ToArray();
                    consumableItems[id].RemoveRange(0, count);
                    return array as T[];
                }
                break;
            case ItemType.Weapon:
                if (consumableItems.ContainsKey(id))
                {
                    WeaponBase[] array = this.weapons[id].GetRange(0, count).ToArray();
                    weapons[id].RemoveRange(0, count);
                    return array as T[];
                }
                break;
            case ItemType.Projectile:
                if (consumableItems.ContainsKey(id))
                {
                    Projectile[] array = this.projectiles[id].GetRange(0, count).ToArray();
                    projectiles[id].RemoveRange(0, count);
                    return array as T[];
                }
                break;
        }
        return null;
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
            case ItemType.Projectile:

                if (!projectiles.ContainsKey(id))
                {
                    return;
                }

                projectiles[id].RemoveAt(0);
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
