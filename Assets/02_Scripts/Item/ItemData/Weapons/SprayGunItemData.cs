using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SprayGunItemData", menuName = "Item Data/Gun ItemData/SprayGunItemData", order = 1)]
public class SprayGunItemData : GunItemData
{
    public override ItemBase CreateItem()
    {
        GameObject gameObject = new GameObject(this.itemName);
        SprayGun sprayGun = gameObject.AddComponent<SprayGun>();

        gameObject.transform.parent = this.weaponSpaceTransform.transform;

        sprayGun.sprayGunItemData = this;

        return sprayGun;
    }

    public override ItemData SetData(ItemData itemData)
    {
        if (itemData is SprayGunItemData)
        {
            SprayGunItemData pistolItemData = (SprayGunItemData)itemData;
            base.SetData(itemData);
            return this;
        }
        else
        {
            return null;
        }
    }
}
