using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFactory : Singleton<EnemyFactory>
{
    Dictionary<string, GameObject> enemeyPrefabs = new Dictionary<string, GameObject>(); 

    public List<GameObject> enemyPrefabsList { get { return _enemyPrefabsList; } set { _enemyPrefabsList = value; } }
    [SerializeField] List<GameObject> _enemyPrefabsList = new List<GameObject>();

    public List<EnemyBaseData> enemyBaseDatas { get { return _enemyBaseDatas; } set { _enemyBaseDatas = value; } }
    [SerializeField] List<EnemyBaseData> _enemyBaseDatas = new List<EnemyBaseData>();

    public bool isInitialized{ get { return _isInitialized; } }
    private bool _isInitialized = false;

    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        yield return new WaitUntil(() => SceneHandler.instance.loadingScenes.Count == 0);

        for (int i = 0; i < _enemyBaseDatas.Count; i++)
        {
            string unitName = _enemyBaseDatas[i].unitName;

            ObjectPoolManager.instance.CreateObjectPool(unitName + " Pool");
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
        GameObject gameObject = Instantiate(enemeyPrefabs[unitName]);
        ObjectPoolManager.instance.ReturnObject(unitName + " Pool", gameObject);
        return gameObject.GetComponent<EnemyBase>();
    }


}
