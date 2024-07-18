using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemComponent : MonoBehaviour
{
    [SerializeField] float rotSpd = 100;
    [SerializeField] float moveSpd = 0.15f;
    [SerializeField] float risePos = 0.3f;

    public ItemData itemData;

    bool isRising = true;
    bool isDestroied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroied)
        {
            return;
        }
        Player player = other.transform.GetComponentInParent<Player>();
        if (player != null)
        {
            isDestroied = true;
            itemData.unitUser = player;
            player.GiveItem(itemData);
            Destroy(this.gameObject);
        }
        
    }
    private void Awake()
    {
        itemData = Instantiate(itemData);
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
        Debug.Log(name + ": "+ rotSpd);

    }

}
