using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{

    Dictionary<string, ObjectPool> objectPools = new Dictionary<string, ObjectPool>();


    void Start()
    {
           
    }

    void Update()
    {
        
    }

    ObjectPool CreateObjectPool(string poolName)
    {
        ObjectPool objectPool = new GameObject().AddComponent<ObjectPool>();
        objectPools.Add(poolName, objectPool);

        return objectPool;
    }

    GameObject GetObject(string poolName)
    {
        objectPools[poolName].GetObject();

        return objectPools[poolName].GetObject();
    }


}
