using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    Dictionary<string, ObjectPool> objectPools = new Dictionary<string, ObjectPool>();

    public ObjectPool CreateObjectPool(string poolName)
    {
        Debug.Log("오브젝트풀 생성완료");
        GameObject poolObject = new GameObject(poolName);
        ObjectPool objectPool = poolObject.AddComponent<ObjectPool>();
        objectPool.poolName = poolName;
        objectPools.Add(poolName, objectPool);

        return objectPool;
    }

    public GameObject GetObject(string poolName)
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
    public GameObject GetObject(string poolName, Vector3 position)
    {
        if (objectPools.ContainsKey(poolName))
        {
            GameObject gameObject = objectPools[poolName].GetObject();
            gameObject.transform.position = position;
            return gameObject;
        }
        else
        {
            Debug.LogError($"Object pool with name {poolName} does not exist.");
            return null;
        }
    }
    public GameObject GetObject(string poolName, Vector3 position, Quaternion rotation)
    {
        if (objectPools.ContainsKey(poolName))
        {
            GameObject gameObject = objectPools[poolName].GetObject();
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            return gameObject;
        }
        else
        {
            Debug.LogError($"Object pool with name {poolName} does not exist.");
            return null;
        }
    }

    public void ReturnObject(string poolName, GameObject gameObject)
    {
        if (objectPools.ContainsKey(poolName))
        {
            Debug.Log(poolName + "에 반환됨");
            objectPools[poolName].ReturnObject(gameObject);
        }
        else
        {
            Debug.LogError($"Object pool with name {poolName} does not exist.");
        }
    }
    public void ReturnObject(string poolName, GameObject gameObject, float time)
    {
        StartCoroutine(ReturnObjectAfterTime(poolName, gameObject, time));
    }

    private IEnumerator ReturnObjectAfterTime(string poolName, GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);

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
