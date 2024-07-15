using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage1 : IStage
{
    public Stage1()
    {
        SetUp();
    }

    GameObject BasicEnemyPrefab;
    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }

    public Transform target { get {return _target; } set { _target = value; } }

    public List<Transform> spawnPoints = new List<Transform>();

    protected float _timeElapsed = 0;

    Transform stage1PointGroup;
    bool isGenerating;

    Transform _target;

    public void Execute()
    {
        if (isGenerating)
        {
            return;
        }
        CoroutineHelper.instance.RunCoroutine(GenerateAll());
    }

    public void SetUp()
    {
        BasicEnemyPrefab = Resources.Load<GameObject>("Enemies/BasicEnemy");
        target = GameObject.FindWithTag("PLAYER").transform;

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
    }

    void Update()
    {
        
    }
    IEnumerator GenerateAll()
    {
        isGenerating = true;

        yield return    Generate_1();
        yield return new WaitForSeconds(4.0f);

        yield return    Generate_2();
        yield return new WaitForSeconds(4.0f);

                        Generate_3();
        yield return new WaitForSeconds(4.0f);

                        Generate_4();
        yield return new WaitForSeconds(4.0f);

        isGenerating = false;
    }
    IEnumerator Generate_1()
    {

        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i + 1].transform.rotation = Quaternion.Euler(0, 200, 0);
            GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[i + 1]).GetComponent<EnemyBase>().enableSlow = true;
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i + 1].transform.rotation = Quaternion.Euler(0, 160, 0);
            GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[i + 1]).GetComponent<EnemyBase>().enableSlow = true;
            yield return new WaitForSeconds(0.3f);
        }

    }

    IEnumerator Generate_2()
    {

        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].transform.LookAt(target);
            GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[i]).GetComponent<EnemyBase>().enableSlow = true;
            yield return new WaitForSeconds(0.5f);
        }

    }
    void Generate_3()
    {
        
        spawnPoints[0].transform.LookAt(new Vector3(0, 0, 0));
        spawnPoints[6].transform.LookAt(new Vector3(0, 0, 0));
        spawnPoints[3].transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[6]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[3]).GetComponent<EnemyBase>().enableSlow = false;

        //spawnPoints[0].transform.rotation = Quaternion.Euler(0, 180, 0);
        //spawnPoints[6].transform.rotation = Quaternion.Euler(0, 180, 0);

    }

    void Generate_4()
    {
        spawnPoints[0].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPoints[6].transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[1]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[2]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[4]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[5]).GetComponent<EnemyBase>().enableSlow = true;
    }

    void Generate_5()
    {
        spawnPoints[1].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPoints[2].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPoints[3].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPoints[4].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPoints[5].transform.rotation = Quaternion.Euler(0, 180, 0);

        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[1]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[2]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[3]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[4]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[5]).GetComponent<EnemyBase>().enableSlow = true;


    }



}
