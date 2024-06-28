using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage1 : MonoBehaviour, IStage
{
    GameObject BasicEnemyPrefab;
    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }

    public Transform target { get {return _target; } set { _target = value; } }

    public List<Transform> spawnPoints = new List<Transform>();

    protected float _timeElapsed = 0;

    Transform stage1PointGroup;
    bool[] isGenerated = new bool[1];

    Transform _target;

    public void Execute()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= 5)
        {
            Generate_1();
        }
        else if (_timeElapsed >= 10)
        {
            
        }
    }

    public void SetUp()
    {
        BasicEnemyPrefab = Resources.Load<GameObject>("Enemies/BasicEnemy");
        target = GameObject.FindWithTag("PLAYER").transform;

        for (int i = 0; i < 0; i++)
        {
            isGenerated[i] = true;
        }

        if ( BasicEnemyPrefab == null )
        {
            Debug.Log("BasicEnemyPrefab is Null!");
        }
        stage1PointGroup = GameObject.FindGameObjectWithTag("STAGE_SPAWN_POINT_GROUP")?.transform.Find("Stage1");
        
        for(int i = 0; i < stage1PointGroup.childCount; i++)
        {
            Transform child = stage1PointGroup.Find(SpawnPoint.commonName + i);
            
            spawnPoints.Add(child);
        }
        
    }
    

    void Start()
    {
        SetUp();
    }

    void Update()
    {
        
    }
    
    void Generate_1()
    {
        if (!isGenerated[0])
        {
            spawnPoints[0].transform.LookAt(target);
            spawnPoints[6].transform.LookAt(target);
            Instantiate(BasicEnemyPrefab, spawnPoints[0]);
            Instantiate(BasicEnemyPrefab, spawnPoints[6]);
            Debug.Log("½ºÆùµÊ");
            isGenerated[0] = true;
        }
    }

}
