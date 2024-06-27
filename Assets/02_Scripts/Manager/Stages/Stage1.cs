using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage1 : MonoBehaviour, IStage
{
    GameObject BasicEnemyPrefab;
    public IPlayer player { get ; set ; }
    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }
    
    public List<Transform> spawnPoints = new List<Transform>();

    protected float _timeElapsed = 0;

    Transform stage1PointGroup;

    public void Execute()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= 5) 
        {
            if (BasicEnemyPrefab != null)
            {
                for(int i = 0; i < spawnPoints.Count; i++)
                {
                    GameObject gameObject = GameObject.Instantiate<GameObject>(BasicEnemyPrefab, spawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));
                    BasicEnemy basicEnemy = gameObject.GetComponent<BasicEnemy>();
                }
                _timeElapsed = 0;
            }
        }
    }

    public void SetUp()
    {
        BasicEnemyPrefab = Resources.Load<GameObject>("Enemies/BasicEnemy");
        Debug.Log("Start 호출됨");
        if ( BasicEnemyPrefab == null )
        {
            Debug.Log("BasicEnemyPrefab is Null!");
        }
        stage1PointGroup = GameObject.FindGameObjectWithTag("STAGE_SPAWN_POINT_GROUP")?.transform.Find("Stage1");
        
        for(int i = 0; i < stage1PointGroup.childCount; i++)
        {
            Transform child = stage1PointGroup.GetChild(i);
            spawnPoints.Add(child);
        }
        
    }
    

    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Instantiate1()
    {
        Instantiate(BasicEnemyPrefab);
    }

}
