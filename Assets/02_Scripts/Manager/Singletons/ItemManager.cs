using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance == this)
            {
                return;
            }
            Debug.Log("Particle duplication");
            Destroy(this.gameObject);
        }
    }

    public GameObject[] itemPrefab;

    public void MakeItem(Transform parent, bool isRandom = true)
    {
        int tmp = Random.Range(0, 2);

        switch (tmp)
        {
            case 0:
                Instantiate<GameObject>(itemPrefab[0], parent.transform.position, parent.transform.rotation);

                break;
                
            case 1:

                Instantiate<GameObject>(itemPrefab[1], parent.transform.position, parent.transform.rotation);
                break;

            case 2:

                break;
                //Instantiate<GameObject>(itemPrefab[0]);
            default:
                break;
        }
    }
    void MakeItem(bool isRandom = true)
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
