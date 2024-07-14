using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PistolItemData", menuName = "Item Data/Gun ItemData/PistolItemData", order = 1)]
public class PistolItemData : GunItemData
{

    public override ItemBase CreateItem()
    {
        GameObject gameObject = new GameObject(this.itemName);
        Pistol pistol = gameObject.AddComponent<Pistol>();

        gameObject.transform.parent = this.weaponSpaceTransform.transform;
        
        pistol.pistolItemData = this;


        return pistol;
    }
    public override ItemData SetData(ItemData itemData)
    {
        if (itemData is PistolItemData)
        {
            PistolItemData pistolItemData = (PistolItemData)itemData;
            base.SetData(itemData);

            return this;
        }
        else
        {
            return null;
        }

    }
}
