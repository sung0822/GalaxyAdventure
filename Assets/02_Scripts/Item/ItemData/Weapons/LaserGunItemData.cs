using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserGunItemData", menuName = "Item Data/Gun ItemData/LaserGunItemData", order = 1)]
public class LaserGunItemData : GunItemData
{
    public override ItemBase CreateItem()
    {
        GameObject gameObject = new GameObject(this.itemName);
        LaserGun laserGun = gameObject.AddComponent<LaserGun>();

        laserGun.laserGunItemData = this;

        gameObject.transform.parent = this.weaponSpaceTransform.transform;
        gameObject.transform.localPosition = Vector3.zero;
        laserGun.laserGunItemData = this;

        return laserGun;
    }
    public override ItemData SetData(ItemData itemData)
    {
        if (itemData is LaserGunItemData)
        {
            LaserGunItemData laserGunItemData = (LaserGunItemData)itemData;
            base.SetData(itemData);

            return this;
        }
        else
        {
            return null;
        }

    }
}
