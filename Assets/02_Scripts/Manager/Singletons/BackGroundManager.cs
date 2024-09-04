using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackGroundManager : Singleton<BackGroundManager>
{

    public GameObject[] cloudPrefabs;
    public List<Mover> cloudPool;

    public GameObject[] rockPrefabs { get { return _rockPrefab; } set { _rockPrefab = value; } }
    [SerializeField] GameObject[] _rockPrefab;

    public List<Mover> rockPool;

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

    public GameObject background { get { return _background; } set { _background = value; } }
    [SerializeField] GameObject _background;

    [SerializeField] MeshRenderer backgroundRenderer;
    [SerializeField] Material backgroundMaterial;
    [SerializeField] Color changedBackgroundColor;

    public float minSpd { get{ return _minSpd;} set { _minSpd = value; } }
    [SerializeField] float _minSpd;

    public float maxSpd { get { return _maxSpd; } set { _maxSpd = value; } }
    [SerializeField] float _maxSpd;

    public float originalMinSpd { get { return _originalMinSpd; } }
    [SerializeField] private float _originalMinSpd;

    public float originalMaxSpd { get { return _originalMaxSpd; } }
    [SerializeField] private float _originalMaxSpd;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Awake 호출됨");
        _originalMaxSpd = maxSpd;
        _originalMinSpd = minSpd;
    }


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

    public void SpawnBackObject(GameObject gameObject)
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

        Mover mover = gameObject.GetComponent<Mover>();

        float spd = Random.Range(minSpd, maxSpd);

        mover.moveSpd = spd;

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
            
            Mover Cloudmover = cloud.GetComponent<Mover>();
            cloudPool.Add(Cloudmover);

            SpawnBackObject(cloud);
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
        for (int i = 0; i < maxRocks; i++)
        {
            int idx = Random.Range(0, rockPrefabs.Length);
            var rock = Instantiate<GameObject>(rockPrefabs[idx]);
            SpawnRock(rock);
            rock.name = $"Rock{i:00}";

            Mover rockMover = rock.GetComponent<Mover>();

            rockPool .Add(rockMover);
            SpawnRock(rock);
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

    public void SetAllCloudsSpd()
    {

    }

    public void IncreaseOrDeacreseSpdOfBackground(float spd)
    {
        for (int i = 0; i < cloudPool.Count; i++)
        {
            cloudPool[i].moveSpd += spd;

        }
        for (int i = 0; i < rockPool.Count; i++)
        {
            rockPool[i].moveSpd += spd;
        }
    }

    public void AdjustSpdOfBackground(float spd, float duration)
    {
        for (int i = 0; i < cloudPool.Count; i++)
        {
            cloudPool[i].AdjustSpd(spd, duration);
        }

        for (int i = 0; i < rockPool.Count; i++)
        {
            Debug.Log("AdjustSpd 호출, purpose spd is " + spd);
            rockPool[i].AdjustSpd(spd, duration);
        }
    }

    public void StopCreating()
    {
        StopCoroutine(rockCoroutine);
        StopCoroutine(cloudCoroutine);
    }
}
