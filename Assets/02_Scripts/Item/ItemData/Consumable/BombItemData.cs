using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BombItemData", menuName = "Item Data/Consumable Items/BombItemData", order = 1)]
public class BombItemData : ConsumableItemData
{
    public GameObject bomber { get { return _bomber; } set { _bomber = value; } }
    [SerializeField] private GameObject _bomber;

    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    [SerializeField] private TeamType _teamType;

    public override ItemBase CreateItem()
    {
        GameObject gameObject = new GameObject(this.itemName);
        BombItem bomb = gameObject.AddComponent<BombItem>();

        bomb.consumableItemData = this;


        return bomb;
    }
}
