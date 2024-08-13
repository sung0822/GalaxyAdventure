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
    public float timeElapsed { get { return _timeElapsed; }}
    protected float _timeElapsed = 0;

    public Transform target { get { return _target; } set { _target = value; } }
    Transform _target;

    public List<Transform> spawnPointsForward = new List<Transform>();
    public List<Transform> spawnPointsBackward = new List<Transform>();

    Transform pointGroupForward;
    Transform pointGroupBackward;
    Transform pointGroup;

    bool isGenerating;

    public Coroutine currentCoroutine { get { return _currentCoroutine; } }
    Coroutine _currentCoroutine;

    public void Execute()
    {
        if (isGenerating)
        {
            return;
        }
        Debug.Log("생성 시작");
        _currentCoroutine = CoroutineHelper.instance.RunCoroutine(GenerateAll());
    }
    public void StopGenerating()
    {
        CoroutineHelper.instance.StopRunningCoroutine(_currentCoroutine);
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
        
        pointGroup = GameObject.FindGameObjectWithTag("STAGE_SPAWN_POINT_GROUP").transform;
        pointGroupForward = pointGroup.transform.Find("Forward");
        pointGroupBackward = pointGroup.transform.Find("Backward");

        
        for (int i = 0; i < pointGroupForward.childCount; i++)
        {
            Transform child = pointGroupForward.Find(SpawnPoint.commonName + i);
            if (child == null)
            {
                Debug.Log("null");
            }
            spawnPointsForward.Add(child);
        }

        for (int i = 0; i < pointGroupBackward.childCount; i++)
        {
            Transform child = pointGroupBackward.Find(SpawnPoint.commonName + i);
            spawnPointsBackward.Add(child);
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

        yield return Generate_4();
        yield return new WaitForSeconds(4.0f);



        isGenerating = false;
    }
    IEnumerator Generate_1()
    {
        Debug.Log("Stage2: Generate1 Called");
        spawnPointsForward[0].transform.LookAt(MainManager.instance.transform);
        spawnPointsForward[6].transform.LookAt(MainManager.instance.transform );
        
        spawnPointsForward[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[1].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[5].transform.localRotation = Quaternion.Euler(0, 0, 0);

        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[3]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPointsForward[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPointsForward[6]).GetComponent<EnemyBase>().enableSlow = true;
        
        yield return new WaitForSeconds(2.0f);
        GameObject.Instantiate(stealthEnemyPrefab, spawnPointsForward[1]).GetComponent<EnemyBase>().enableSlow = true;
        yield return new WaitForSeconds(2.0f);
        GameObject.Instantiate(stealthEnemyPrefab, spawnPointsForward[5]).GetComponent<EnemyBase>().enableSlow = true;
    }

    IEnumerator Generate_2()
    {
        Debug.Log("Stage2: Generate2 Called");
        for (int i = 0; i < 5; i++)
        {
            spawnPointsForward[i].transform.LookAt(target);
            GameObject.Instantiate(eaglePrefab, spawnPointsForward[i]).GetComponent<EnemyBase>().enableSlow = true;
            yield return new WaitForSeconds(0.5f);
        }

    }
    void Generate_3()
    {
        Debug.Log("Stage2: Generate3 Called");
        //중앙을 향해 달려가는 기본 몬스터
        spawnPointsForward[0].transform.LookAt(MainManager.instance.transform);
        spawnPointsForward[6].transform.LookAt(MainManager.instance.transform);
        
        spawnPointsForward[3].transform.localRotation = Quaternion.Euler(0, 180, 0);
        
        spawnPointsForward[1].transform.LookAt(MainManager.instance.transform);
        spawnPointsForward[5].transform.LookAt(MainManager.instance.transform);
        
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[6]).GetComponent<EnemyBase>().enableSlow = true;
        
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[3]).GetComponent<EnemyBase>().enableSlow = false;

        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[1]).GetComponent<EnemyBase>().enableSlow = false;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[5]).GetComponent<EnemyBase>().enableSlow = false;

        //spawnPoints[1].transform.rotation = Quaternion.Euler(0, 180, 0);
        //spawnPoints[5].transform.rotation = Quaternion.Euler(0, 180, 0);

    }

    IEnumerator Generate_4()
    {
        Debug.Log("Stage2: Generate4 Called");
        spawnPointsForward[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[1].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[2].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[4].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[5].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[6].transform.localRotation = Quaternion.Euler(0, 0, 0);

        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[6]).GetComponent<EnemyBase>().enableSlow = true;
        
        yield return new WaitForSeconds(1.0f);

        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[1]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[5]).GetComponent<EnemyBase>().enableSlow = true;

        yield return new WaitForSeconds(1.0f);
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[2]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[4]).GetComponent<EnemyBase>().enableSlow = true;
        yield return new WaitForSeconds(0.5f);
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[3]).GetComponent<EnemyBase>().enableSlow = true;
        
        GameObject.Instantiate(eaglePrefab, spawnPointsForward[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPointsForward[1]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPointsForward[2]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPointsForward[4]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPointsForward[5]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(eaglePrefab, spawnPointsForward[6]).GetComponent<EnemyBase>().enableSlow = true;
    }

    void Generate_5()
    {
        Debug.Log("Stage2: Generate5 Called");
        spawnPointsForward[1].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[2].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[4].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[5].transform.localRotation = Quaternion.Euler(0, 0, 0);

        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[1]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[2]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[3]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[4]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[5]).GetComponent<EnemyBase>().enableSlow = true;


    }



}
