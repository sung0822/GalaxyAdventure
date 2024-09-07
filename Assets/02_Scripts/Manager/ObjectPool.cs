using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public string poolName { get { return _poolName; } set { _poolName = value; } }
    [SerializeField] string _poolName;
    private Queue<GameObject> objects = new Queue<GameObject>();
    public void AddObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        objects.Enqueue(gameObject);
    }
    public GameObject GetObject()
    {
        if (objects.Count > 0)
        {
            GameObject obj = objects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.Log("there is no objects in the pool");
            return null;
        }

    }
    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        objects.Enqueue(gameObject);
    }
}
