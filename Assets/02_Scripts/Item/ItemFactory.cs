using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFactory : MonoBehaviour
{
    public abstract T CreateItem<T>(ItemData itemData) where T : ItemBase;
    public abstract ItemBase CreateItem(ItemData itemData);
}
