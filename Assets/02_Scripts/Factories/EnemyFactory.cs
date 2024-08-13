using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : Singleton<EnemyFactory>
{
    Dictionary<string, GameObject> enemeyPrefabs = new Dictionary<string, GameObject>(); 

    public List<GameObject> enemyPrefabsList { get { return _enemyPrefabsList; } set { _enemyPrefabsList = value; } }
    [SerializeField] List<GameObject> _enemyPrefabsList = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < _enemyPrefabsList.Count; i++)
        {
            string unitName = _enemyPrefabsList[i].GetComponent<UnitBase>().unitName;
            ObjectPoolManager.instance.CreateObjectPool(unitName + " Pool");
            enemeyPrefabs.Add(unitName, _enemyPrefabsList[i]);
        }
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
