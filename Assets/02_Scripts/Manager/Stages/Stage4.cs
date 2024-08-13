using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4 : IStage
{
    public Stage4()
    {
        SetUp();
    }

    GameObject BasicEnemyPrefab;
    GameObject eaglePrefab;
    GameObject stealthEnemyPrefab;
    GameObject bigSizeEnemyprefab;
    GameObject sprayEnemyPrefab;

    public float timeElapsed { get { return _timeElapsed; } }
    protected float _timeElapsed = 0;

    public Transform target { get { return _target; } set { _target = value; } }
    Transform _target;

    public List<Transform> spawnPointsForward = new List<Transform>();
    public List<Transform> spawnPointsBackward = new List<Transform>();
    public List<Transform> spawnPointsLeftward = new List<Transform>();
    public List<Transform> spawnPointsRightward = new List<Transform>();

    Transform pointGroupForward;
    Transform pointGroupBackward;
    Transform pointGroupLeftward;
    Transform pointGroupRightward;
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
        bigSizeEnemyprefab = Resources.Load<GameObject>("Enemies/BigSizeEnemy");
        sprayEnemyPrefab = Resources.Load<GameObject>("Enemies/sprayEnemy");

        target = GameObject.FindWithTag("PLAYER").transform;

        if (BasicEnemyPrefab == null)
        {
            Debug.Log("BasicEnemyPrefab is Null!");
        }

        pointGroup = GameObject.FindGameObjectWithTag("STAGE_SPAWN_POINT_GROUP").transform;
        pointGroupForward = pointGroup.transform.Find("Forward");
        pointGroupBackward = pointGroup.transform.Find("Backward");
        pointGroupLeftward = pointGroup.transform.Find("Leftward");
        pointGroupRightward = pointGroup.transform.Find("Rightward");


        for (int i = 0; i < pointGroupForward.childCount; i++)
        {
            Transform child = pointGroupForward.Find(SpawnPoint.commonName + i);
            spawnPointsForward.Add(child);
        }

        for (int i = 0; i < pointGroupBackward.childCount; i++)
        {
            Transform child = pointGroupBackward.Find(SpawnPoint.commonName + i);
            spawnPointsBackward.Add(child);
        }

        for (int i = 0; i < pointGroupLeftward.childCount; i++)
        {
            Transform child = pointGroupLeftward.Find(SpawnPoint.commonName + i);
            spawnPointsLeftward.Add(child);
        }

        for (int i = 0; i < pointGroupRightward.childCount; i++)
        {
            Transform child = pointGroupRightward.Find(SpawnPoint.commonName + i);
            spawnPointsRightward.Add(child);
        }
    }

    IEnumerator GenerateAll()
    {
        isGenerating = true;

        yield return Generate_1();
        yield return new WaitForSeconds(4.0f);

        yield return Generate_2();
        yield return new WaitForSeconds(4.0f);

        CoroutineHelper.instance.RunCoroutine(Generate_3());
        yield return new WaitForSeconds(4.0f);

        yield return Generate_4();
        yield return new WaitForSeconds(4.0f);



        isGenerating = false;
    }
    IEnumerator Generate_1()
    {
        spawnPointsForward[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
        GameObject.Instantiate(sprayEnemyPrefab, spawnPointsForward[3]).GetComponent<EnemyBase>().enableSlow = true;
        
        yield return new WaitForSeconds(0.5f);
        
        spawnPointsBackward[6].transform.LookAt(MainManager.instance.transform);
        GameObject.Instantiate(sprayEnemyPrefab, spawnPointsBackward[6]).GetComponent<EnemyBase>().enableSlow = true;
        
        yield return new WaitForSeconds(0.5f);
        
        spawnPointsBackward[0].transform.LookAt(MainManager.instance.transform);
        GameObject.Instantiate(sprayEnemyPrefab, spawnPointsBackward[0]).GetComponent<EnemyBase>().enableSlow = true;

    }

    IEnumerator Generate_2()
    {

        spawnPointsForward[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
        GameObject.Instantiate(bigSizeEnemyprefab, spawnPointsForward[3]).GetComponent<EnemyBase>().enableSlow = true;

        yield return new WaitForSeconds(0.5f);

        spawnPointsForward[0].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[1].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[2].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[3].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[4].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[5].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[6].transform.rotation = Quaternion.Euler(0, 0, 0);
        for (int i = 1; i < 6; i++)
        {
            spawnPointsForward[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[0]).GetComponent<EnemyBase>().enableSlow = true;
        }
    }
    IEnumerator Generate_3()
    {
        spawnPointsLeftward[2].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsLeftward[4].transform.localRotation = Quaternion.Euler(0, 0, 0);
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsLeftward[2]).GetComponent<EnemyBase>().enableSlow = true;
        yield return new WaitForSeconds(0.5f);

        spawnPointsRightward[4].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsRightward[2].transform.localRotation = Quaternion.Euler(0, 0, 0);
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsRightward[4]).GetComponent<EnemyBase>().enableSlow = true;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++)
        {
            // 뒤에서 기본 3마리 등장
            spawnPointsBackward[i + 2].transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameObject.Instantiate(BasicEnemyPrefab, spawnPointsBackward[i + 2]).GetComponent<EnemyBase>().enableSlow = true;
        }

        yield return new WaitForSeconds(0.5f);

        spawnPointsForward[2].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[4].transform.localRotation = Quaternion.Euler(0, 0, 0);
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[4]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[2]).GetComponent<EnemyBase>().enableSlow = true;

        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsRightward[2]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsRightward[4]).GetComponent<EnemyBase>().enableSlow = true;

        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsLeftward[2]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsLeftward[4]).GetComponent<EnemyBase>().enableSlow = true;
        yield return new WaitForSeconds(0.5f);

    }

    IEnumerator Generate_4()
    {
        Debug.Log("Stage2: Generate4 Called");
        spawnPointsForward[0].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[1].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[2].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[3].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[4].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[5].transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[6].transform.rotation = Quaternion.Euler(0, 0, 0);

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
        spawnPointsForward[1].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPointsForward[2].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPointsForward[3].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPointsForward[4].transform.rotation = Quaternion.Euler(0, 180, 0);
        spawnPointsForward[5].transform.rotation = Quaternion.Euler(0, 180, 0);

        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[0]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[1]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[2]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[3]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[4]).GetComponent<EnemyBase>().enableSlow = true;
        GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[5]).GetComponent<EnemyBase>().enableSlow = true;


    }



}
