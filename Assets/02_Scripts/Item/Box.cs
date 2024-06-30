using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IBox
{
    IItem item;
    Material material;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "PLAYER")
        {
            if(item == null)
            {
                Debug.Log("Item is null");
                return;
            }
            other.transform.root.GetComponent<PlayerCtrl>().GivePlayerItem(item);
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;

        int tmp =  Random.Range(1, 3);
        

        switch (tmp) 
        {
            case 1:
                item = new HealItem();
                material.color = Color.white;
                break;
            
            case 2:
                item = new ImmortalItem();
                material.color = Color.yellow;
                break;

            case 3:
                item = new BombItem();
                material.color = Color.black;
                break;


        }

        if (item == null) 
        {
            Debug.Log("아이템이 Null임 ");
        }
    }

    void Start()
    {
        
    }

}
