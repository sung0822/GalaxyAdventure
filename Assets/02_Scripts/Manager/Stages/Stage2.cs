using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : IStage
{
    public Stage2()
    {
        SetUp();
    }

    GameObject BasicEnemyPrefab;
    GameObject eaglePrefab;
    GameObject stealthEnemyPrefab;
    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }

    public Transform target { get { return _target; } set { _target = value; } }

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
        stealthEnemyPrefab = Resources.Load<GameObject>("Enemies/StealthEnemy");
        eaglePrefab = Resources.Load<GameObject>("Enemies/Eagle");

        target = GameObject.FindWithTag("PLAYER").transform;

        if (BasicEnemyPrefab == null)
        {
            Debug.Log("BasicEnemyPrefab is Null!");
        }
        stage1PointGroup = GameObject.FindGameObjectWithTag("STAGE_SPAWN_POINT_GROUP")?.transform.Find("Stage1");

        for (int i = 0; i < stage1PointGroup.childCount; i++)
        {
            Transform child = stage1PointGroup.Find(SpawnPoint.commonName + i);
            spawnPoints.Add(child);
        }
    }

    IEnumerator GenerateAll()
    {
        isGenerating = true;

        yield return Generate_1();
        yield return new WaitForSeconds(4.0f);

        yield return Generate_2();
        yield return new WaitForSeconds(4.0f);

        Generate_3();
        yield return new WaitForSeconds(4.0f);



        isGenerating = false;
    }
    IEnumerator Generate_1()
    {
        spawnPoints[0].transform.LookAt(Vector3.zero);
        spawnPoints[6].transform.LookAt(Vector3.zero);
        spawnPoints[3].transform.rotation = Quaternion.Euler(0, 180, 0);
        
        spawnPoints[1].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPoints[5].transform.rotation = Quaternion.Euler(0, 180, 0);

        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[3]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPoints[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPoints[6]).GetComponent<EnemyBase>().enableSlow = true;
        
        yield return new WaitForSeconds(2.0f);
        GameObject.Instantiate(stealthEnemyPrefab, spawnPoints[1]).GetComponent<EnemyBase>().enableSlow = true;
        yield return new WaitForSeconds(2.0f);
        GameObject.Instantiate(stealthEnemyPrefab, spawnPoints[5]).GetComponent<EnemyBase>().enableSlow = true;
    }

    IEnumerator Generate_2()
    {

        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].transform.LookAt(target);
            GameObject.Instantiate(eaglePrefab, spawnPoints[i]).GetComponent<EnemyBase>().enableSlow = true;
            yield return new WaitForSeconds(0.5f);
        }

    }
    void Generate_3()
    {

        spawnPoints[0].transform.LookAt(Vector3.zero);
        spawnPoints[6].transform.LookAt(Vector3.zero);
        
        spawnPoints[3].transform.rotation = Quaternion.Euler(0, 180, 0);
        
        spawnPoints[1].transform.LookAt(Vector3.zero);
        spawnPoints[5].transform.LookAt(Vector3.zero);
        
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[6]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[3]).GetComponent<EnemyBase>().enableSlow = false;

        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[1]).GetComponent<EnemyBase>().enableSlow = false;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPoints[5]).GetComponent<EnemyBase>().enableSlow = false;

        //spawnPoints[1].transform.rotation = Quaternion.Euler(0, 180, 0);
        //spawnPoints[5].transform.rotation = Quaternion.Euler(0, 180, 0);

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
