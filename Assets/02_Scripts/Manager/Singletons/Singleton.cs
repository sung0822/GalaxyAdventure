using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private T _instance;

    public T instance
    {
        get 
        {
            if (instance == null)
            {
                GameObject obj;
                obj = GameObject.Find(typeof(T).Name);
                if (obj == null)
                {
                    obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
                else
                {
                    _instance = obj.GetComponent<T>();
                }
            }
            return _instance; 
        }
    }
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
