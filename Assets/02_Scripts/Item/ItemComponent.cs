using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemComponent : MonoBehaviour
{
    [SerializeField] float rotSpd = 100;
    [SerializeField] float moveSpd = 0.15f;
    [SerializeField] float risePos = 0.3f;
    private float previousRot = 0;

    [SerializeField] public int itemId { get { return _itemId; } }
    [SerializeField] private int _itemId;

    public ItemData itemData;

    bool isRising = true;
    bool isDestroied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "PLAYER")
        {
            if (isDestroied)
            {
                Debug.Log("¾È¸Ô¾îÁü");
                return;
            }
            isDestroied = true;
            other.transform.root.GetComponent<Player>().GiveItem(itemData);
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        if(itemData.itemType == ItemType.Consumable)
        {
            switch (itemId)
            {
                case 0:

                    break;
                case 1:

                    //itemData = new ImmortalItem(null);
                    break;
                case 2:

                    //itemData = new BombItem(null);
                    break;

            }
        }
        else if (itemData.itemType == ItemType.Weapon)
        {
            switch (itemId)
            {
                case 0:

                    // itemData = 
                    break;
                case 1:

                    //itemData = new ImmortalItem(null);
                    break;
                case 2:

                    //itemData = new BombItem(null);
                    break;

            }
        }
    }


    void Update()
    {
        
        
        if (transform.localPosition.y >= risePos)
            isRising = false;
        else if (transform.localPosition.y <= 0f)
            isRising = true;

        if (isRising)
            transform.Translate(0, +1 * moveSpd * Time.deltaTime, 0);
        else
            transform.Translate(0, -1 * moveSpd * Time.deltaTime, 0);

        if (itemData.itemType == ItemType.Weapon)
        {
            return;
        }

        transform.Rotate(0, rotSpd * Time.deltaTime, 0);

    }

}
