using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFactory : Singleton<EnemyFactory>
{
    Dictionary<string, GameObject> enemeyPrefabs = new Dictionary<string, GameObject>(); 

    public List<GameObject> enemyPrefabsList { get { return _enemyPrefabsList; } set { _enemyPrefabsList = value; } }
    [SerializeField] List<GameObject> _enemyPrefabsList = new List<GameObject>();
    public bool isInitialized{ get { return _isInitialized; } }
    private bool _isInitialized = false;

    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        yield return new WaitUntil(() => SceneHandler.instance.loadingScenes.Count == 0);
        for (int i = 0; i < _enemyPrefabsList.Count; i++)
        {
            string unitName = _enemyPrefabsList[i].GetComponent<UnitBase>().unitName;
            ObjectPoolManager.instance.CreateObjectPool(unitName + " Pool");
            Debug.Log("Object Pool name is : " + unitName + " Pool");
            enemeyPrefabs.Add(unitName, _enemyPrefabsList[i]);
        }
        _isInitialized = true;
    }

    public EnemyBase CreateEnemy(string unitName)
    {
        if (!enemeyPrefabs.ContainsKey(unitName))
        {
            Debug.LogError("존재하지 않는 유닛 이름");
            return null;
        }
        Debug.Log("Enemy " + unitName + " has been created");
        GameObject gameObject = Instantiate(enemeyPrefabs[unitName]);
        ObjectPoolManager.instance.ReturnObject(unitName + " Pool", gameObject);
        return gameObject.GetComponent<EnemyBase>();
    }


}
