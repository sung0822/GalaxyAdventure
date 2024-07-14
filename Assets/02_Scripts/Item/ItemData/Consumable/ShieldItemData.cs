using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield ItemData", menuName = "Item Data/Consumable Items/Shield ItemData", order = 1)]
public class ShieldItemData : ConsumableItemData
{

    public float shieldTerm { get {return _shieldTerm;} }
    [SerializeField] float _shieldTerm;

    public GameObject barrierPrefab { get { return _barrierPrefab; } }
    [SerializeField] GameObject _barrierPrefab;

    public override ItemBase CreateItem()
    {
        GameObject gameObject = new GameObject(this.itemName);
        ShieldItem shield = gameObject.AddComponent<ShieldItem>();

        shield.consumableItemData = this;

        return shield;
    }

    
}
