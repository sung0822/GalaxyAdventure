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

    public IItem item;

    bool isRising = true;
    bool isDestroied = false;

    Material material;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "PLAYER")
        {
            if (isDestroied)
            {
                return;
            }
            isDestroied = true;
            other.transform.root.GetComponent<PlayerCtrl>().GivePlayerItem(item);
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        switch(itemId)
        {
            case 0:

                item = new HealItem();
                break;
            case 1:
                
                item = new ImmortalItem();
                break;
            case 2:

                item = new BombItem();
                break;
                
        }
    }


    void Update()
    {
        transform.Rotate(0, rotSpd * Time.deltaTime, 0);
        
        if (transform.localPosition.y >= risePos)
            isRising = false;
        else if (transform.localPosition.y <= 0f)
            isRising = true;

        if (isRising)
            transform.localPosition = new Vector3(0, transform.localPosition.y + 1 * moveSpd * Time.deltaTime, 5);
        else
            transform.localPosition = new Vector3(0, transform.localPosition.y - 1 * moveSpd * Time.deltaTime, 5);
        
        
    }

}
