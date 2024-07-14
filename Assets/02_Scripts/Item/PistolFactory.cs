using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFactory : ItemFactory
{
    public override ItemBase CreateItem(ItemData itemData)
    {
        GameObject gameObject = new GameObject(itemData.itemName);

        Pistol item = gameObject.AddComponent<Pistol>();
        item.gunItemData = (GunItemData)itemData;
        return item;
    }

    public override T CreateItem<T>(ItemData itemData)
    {
        throw new System.NotImplementedException();
    }
}
