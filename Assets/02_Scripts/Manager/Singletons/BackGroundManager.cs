using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public static BackGroundManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance == this)
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }

    public GameObject[] cloudPrefabs;
    public List<GameObject> cloudPool;

    public GameObject[] rockPrefabs { get { return _rockPrefab; } set { _rockPrefab = value; } }
    [SerializeField] GameObject[] _rockPrefab;

    public List<GameObject> rockPool;

    [SerializeField] int maxRocks = 10;
    [SerializeField] int maxClouds = 20;

    Coroutine cloudCoroutine;
    Coroutine rockCoroutine;

    public List<Transform> cloudPoints { get { return _cloudPoints; } set { _cloudPoints = value; } }
    public List<Transform> _cloudPoints = new List<Transform>();

    public List<Transform> rockPoints { get { return _rockPoints; } set { _rockPoints = value; } }
    public List<Transform> _rockPoints = new List<Transform>();

    Transform PointsGroup;

    public Transform cloudPointsGroup { get { return _cloudPointsGroup; } set { _cloudPointsGroup = value; } }
    [SerializeField] private Transform _cloudPointsGroup;

    public Transform rockPointsGroup { get { return _rockPointsGroup; } set { _rockPointsGroup = value; } }
    [SerializeField] private Transform _rockPointsGroup;

    Transform[] formations;

    void Start()
    {
    }
    void Update()
    {
        
    }
    public void SetCloudPointsGroup()
    {

        
        

        for (int i = 0; i < _cloudPointsGroup.childCount; i++)
        {
            Transform child = _cloudPointsGroup.GetChild(i);
            cloudPoints.Add(child);
        }

        for (int i = 0; i < _rockPointsGroup.childCount; i++)
        {
            Transform child = _rockPointsGroup.GetChild(i);
            rockPoints.Add(child);
        }
        
    }

    public void SpawnCloud(GameObject gameObject)
    {
        if (gameObject.tag == null)
        {
            return;
        }
        else if (gameObject.tag == "CLOUD")
        {
            int idx = Random.Range(0, cloudPoints.Count);
            gameObject.transform.position = cloudPoints[idx].position;

        }
        else if(gameObject.tag == "ROCK")
        {
            int idx = Random.Range(0, rockPoints.Count);
            gameObject.transform.position = rockPoints[idx].position;
        }


        float randomScale = Random.Range(0.5f, 1.0f);
        gameObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        float randomRotationY = Random.Range(0f, 360f);
        gameObject.transform.rotation = Quaternion.Euler(0, randomRotationY, 0);

    }
    public void SpawnRock(GameObject gameObject)
    {
        int idx = Random.Range(0, rockPoints.Count);

        gameObject.transform.position = rockPoints[idx].position;

        float randomScale = Random.Range(0.5f, 1.0f);
        gameObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        float randomRotationY = Random.Range(0f, 360f);
        gameObject.transform.rotation = Quaternion.Euler(0, randomRotationY, 0);

        int score = MainManager.Get().score;
    }

    public void CreateClouds()
    {
        cloudCoroutine = StartCoroutine(CreateCloudsDuring());
    }
    IEnumerator CreateCloudsDuring()
    {
        for (int i = 0; i < maxClouds; i++)
        {
            int idx = Random.Range(0, cloudPrefabs.Length);

            var cloud = Instantiate<GameObject>(cloudPrefabs[idx]);
            cloud.name = $"Cloud{i:00}";

            cloudPool.Add(cloud);
            SpawnCloud(cloudPool[i]);
            float waitTime = Random.Range(2.0f, 4.0f);

            yield return new WaitForSeconds(waitTime);
        }
        Debug.Log("CloudPoolCount: " + cloudPool.Count);
    }

    public void CreateRocks()
    {
        rockCoroutine = StartCoroutine(CreateRocksDuring());
    }

    IEnumerator CreateRocksDuring()
    {
        Debug.Log("积己吝");
        for (int i = 0; i < maxRocks; i++)
        {
            int idx = Random.Range(0, rockPrefabs.Length);
            Debug.Log("积己吝");
            var rock = Instantiate<GameObject>(rockPrefabs[idx]);
            SpawnRock(rock);
            rock.name = $"Rock{i:00}";

            rockPool.Add(rock);
            SpawnRock(rockPool[i]);
            float waitTime = Random.Range(2.0f, 4.0f);

            yield return new WaitForSeconds(waitTime);
        }

    }

    public void StopCloudMoving()
    {
        for (int i = 0; i < cloudPool.Count; i++)
        {
            StopCoroutine(cloudCoroutine);
            cloudPool[i].GetComponent<Mover>().moveSpd = 0;
        }
    }
    public void StopRockMoving()
    {
        for (int i = 0; i < rockPool.Count; i++)
        {
            StopCoroutine(rockCoroutine);
            rockPool[i].GetComponent<Mover>().moveSpd = 0;
        }
    }

    private void OnDestroy()
    {
        Mover.InitClouds();
    }
}
