using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoss : IStage
{
    public StageBoss()
    {
        SetUp();
    }
    GameObject BossEnemyPrefab;

    public float timeElapsed { get { return _timeElapsed; } }
    protected float _timeElapsed = 0;

    public Transform target { get { return _target; } set { _target = value; } }
    Transform _target;

    public List<Transform> spawnPointsForward = new List<Transform>();
    public List<Transform> spawnPointsBackward = new List<Transform>();

    Transform pointGroupForward;
    Transform pointGroup;

    bool isGenerated;

    public Coroutine currentCoroutine { get { return _currentCoroutine; } }
    Coroutine _currentCoroutine;

    public void Execute()
    {
        if (isGenerated)
        {
            return;
        }
        CoroutineHelper.instance.RunCoroutine(Generate());
        
        Debug.Log("积己 矫累");
    }

    public void SetUp()
    {

        target = GameObject.FindWithTag("PLAYER").transform;

        BossEnemyPrefab = Resources.Load<GameObject>("Enemies/Boss");
        pointGroup = GameObject.FindGameObjectWithTag("STAGE_SPAWN_POINT_GROUP").transform;
        pointGroupForward = pointGroup.transform.Find("Forward");
        

        for (int i = 0; i < pointGroupForward.childCount; i++)
        {
            Transform child = pointGroupForward.Find(SpawnPoint.commonName + i);
            spawnPointsForward.Add(child);
        }

    }

    public void StopGenerating()
    {
    }
        
    IEnumerator Generate()
    {
        isGenerated = true;
        ItemData itemData = Resources.Load<BombItemData>("Datas/Consumable/BombItemData");
        Player player = GameObject.FindWithTag("PLAYER").GetComponent<Player>();
        player.GiveItem(itemData);

        yield return new WaitForSeconds(5.0f);
        spawnPointsForward[3].transform.localRotation = Quaternion.Euler(0, 0, 0);
        GameObject.Instantiate(BossEnemyPrefab, spawnPointsForward[3]);
        Debug.Log("焊胶 积己");
        BackGroundManager.instance.StopCloudMoving();

    }

}
