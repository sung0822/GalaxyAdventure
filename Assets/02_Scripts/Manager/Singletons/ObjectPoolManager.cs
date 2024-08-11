using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    Dictionary<string, ObjectPool> objectPools = new Dictionary<string, ObjectPool>();

    ObjectPool CreateObjectPool(string poolName)
    {
        GameObject poolObject = new GameObject(poolName);
        ObjectPool objectPool = poolObject.AddComponent<ObjectPool>();
        objectPool.poolName = poolName;
        objectPools.Add(poolName, objectPool);

        return objectPool;
    }

    GameObject GetObject(string poolName)
    {
        if (objectPools.ContainsKey(poolName))
        {
            return objectPools[poolName].GetObject();
        }
        else
        {
            Debug.LogError($"Object pool with name {poolName} does not exist.");
            return null;
        }
    }

    void ReturnObject(string poolName, GameObject gameObject)
    {
        if (objectPools.ContainsKey(poolName))
        {
            objectPools[poolName].ReturnObject(gameObject);
        }
        else
        {
            Debug.LogError($"Object pool with name {poolName} does not exist.");
        }
    }
}
