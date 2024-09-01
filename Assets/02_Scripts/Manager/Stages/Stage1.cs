using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage1 : IStage
{
    public Stage1()
    {
        SetUp();
    }

    public float timeElapsed { get { return _timeElapsed; }}
    protected float _timeElapsed = 0;

    public Transform target { get {return _target; } set { _target = value; } }
    Transform _target;

    public List<Transform> spawnPointsForward = new List<Transform>();
    public List<Transform> spawnPointsBackward = new List<Transform>();

    Transform pointGroup;
    Transform stage1PointGroup;
    bool isGenerating;


    public Coroutine currentCoroutine { get { return _currentCoroutine; } }
    Coroutine _currentCoroutine;

    public void Execute()
    {
        if (isGenerating)
        {
            return;
        }
        _currentCoroutine = CoroutineHelper.instance.RunCoroutine(GenerateAll());
    }


    public void StopGenerating()
    {
        CoroutineHelper.instance.StopRunningCoroutine(currentCoroutine);
    }

    ~Stage1()
    {
    }

    public void SetUp()
    {
        target = GameObject.FindWithTag("PLAYER").transform;

        stage1PointGroup = GameObject.FindGameObjectWithTag("STAGE_SPAWN_POINT_GROUP")?.transform.Find("Forward");
        
        for(int i = 0; i < stage1PointGroup.childCount; i++)
        {
            Transform child = stage1PointGroup.Find(SpawnPoint.commonName + i);
            spawnPointsForward.Add(child);
        }
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
            spawnPointsForward[i + 1].transform.localRotation = Quaternion.Euler(0, 20, 0);
            
            
            ObjectPoolManager.instance.GetObject("BasicEnemy Pool", spawnPointsForward[i+1].position, spawnPointsForward[i+1].rotation);

            //GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[i + 1]).GetComponent<EnemyBase>().enableSlow = true;
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < 5; i++)
        {
            spawnPointsForward[i + 1].transform.localRotation = Quaternion.Euler(0, -20, 0);
            //GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[i + 1]).GetComponent<EnemyBase>().enableSlow = true;
            
            ObjectPoolManager.instance.GetObject("BasicEnemy Pool", spawnPointsForward[i+1].position, spawnPointsForward[i+1].rotation);
            yield return new WaitForSeconds(0.3f);
        }

    }

    IEnumerator Generate_2()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPointsForward[i].transform.LookAt(target);
            
            ObjectPoolManager.instance.GetObject("BasicEnemy Pool", spawnPointsForward[i].position, spawnPointsForward[i].rotation);
            yield return new WaitForSeconds(0.5f);
        }

    }
    void Generate_3()
    {
        
        spawnPointsForward[0].transform.LookAt(MainManager.instance.transform);
        spawnPointsForward[6].transform.LookAt(MainManager.instance.transform);
        spawnPointsForward[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
        
        ObjectPoolManager.instance.GetObject("BasicEnemy Pool",  spawnPointsForward[0].position, spawnPointsForward[0].rotation);

        ObjectPoolManager.instance.GetObject("BasicEnemy Pool", spawnPointsForward[6].position, spawnPointsForward[6].rotation);

        ObjectPoolManager.instance.GetObject("BasicEnemy Pool",  spawnPointsForward[3].position, spawnPointsForward[3].rotation);

    }

    void Generate_4()
    {

        spawnPointsForward[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
        spawnPointsForward[6].transform.localRotation = Quaternion.Euler(0, 0, 0);
        
        //GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[1]);
        //GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[2]);
        //GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[4]);
        //GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[5]);
    }

    void Generate_5()
    {
        for (int i = 0; i < 6; i++)
        {
            spawnPointsForward[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            //GameObject.Instantiate(BasicEnemyPrefab, spawnPointsForward[i]);
        }


    }

}
