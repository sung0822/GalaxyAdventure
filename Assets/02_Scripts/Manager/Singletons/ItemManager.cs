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

    public GameObject[] itemPrefab { get { return _itemPrefab; } set { _itemPrefab = value; } }
    [SerializeField] private GameObject[] _itemPrefab;

    public GameObject MakeItem(Vector3 position, bool isRandom = true)
    {
        int tmp = Random.Range(0, itemPrefab.Length * 4);
        
        if (tmp >= itemPrefab.Length)
        {
            return null;
        }

        GameObject gameObject = Instantiate<GameObject>(itemPrefab[tmp], position, Quaternion.Euler(0, 0, 0));
        gameObject.transform.SetParent(MainManager.instance.mainStage.transform);
        RotateItemObject(gameObject);

        return gameObject;
    }
    public GameObject MakeItem(Vector3 position, int rangeFirst, int rangeMax)
    {
        int tmp = Random.Range(rangeFirst, rangeMax + 1);

        if (tmp >= itemPrefab.Length)
        {
            return null;
        }

        GameObject gameObject = Instantiate<GameObject>(itemPrefab[tmp], MainManager.instance.mainStage.transform.position, Quaternion.Euler(0, 0, 0));
        gameObject.transform.SetParent(MainManager.instance.mainStage.transform);
        
        RotateItemObject(gameObject);

        return gameObject;
    }
    void RotateItemObject(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "WEAPON":
                gameObject.transform.Rotate(0, 90, 50);

                break;
            case "BOX":
                break;

            default:
                gameObject.transform.Rotate(50, 0, 0);
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
