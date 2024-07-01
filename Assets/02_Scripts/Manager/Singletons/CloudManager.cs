using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public static CloudManager instance = null;
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

    public GameObject[] cloudPrefab;
    public List<GameObject> cloudPool;

    int maxCloud = 20;

    void Start()
    {
        CloudMover.SetCloudPointsGroup();
        StartCoroutine(CreateCloud());

    }
    void Update()
    {
        
    }
    IEnumerator CreateCloud()
    {
        for (int i = 0; i < maxCloud; i++)
        {
            int idx = Random.Range(0, cloudPrefab.Length);

            var cloud = Instantiate<GameObject>(cloudPrefab[idx], transform);
            cloud.GetComponent<CloudMover>().Spawn();
            cloud.name = $"Cloud{i:00}";

            cloudPool.Add(cloud);
            //cloudPool[i].GetComponent<CloudMover>().Spawn();
            float waitTime = Random.Range(2.0f, 4.0f);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
