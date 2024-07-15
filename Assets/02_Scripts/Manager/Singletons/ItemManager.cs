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
            Destroy(this.gameObject);
        }
    }

    public GameObject[] itemPrefab;

    public GameObject MakeItem(Transform parent, bool isRandom = true)
    {
        int tmp = Random.Range(0, itemPrefab.Length);
        GameObject gameObject = null;

        gameObject = Instantiate<GameObject>(itemPrefab[tmp], parent.transform.position, Quaternion.Euler(Vector3.zero));
        RotateItemObject(gameObject);

        return gameObject;
    }
    void RotateItemObject(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "WEAPON":
                gameObject.transform.Rotate(0, 90, 50);
                Debug.Log("���� ȣ��");

                break;
            case "BOX":
                Debug.Log("���� ȣ��");
                break;

            default:
                gameObject.transform.Rotate(-50, 0, 0);
                Debug.Log("������ ȣ��");
                break;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
