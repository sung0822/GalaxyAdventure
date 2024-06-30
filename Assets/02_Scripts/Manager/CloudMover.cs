using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public static List<Transform> cloudPoints = new List<Transform>();

    public float moveSpd = 10;
    Transform cloudTransform;
    static Transform cloudPointsGroup;
    static Transform[] formations;


    void Start()
    {
        cloudTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        cloudTransform.Translate(Vector3.forward * -0.001f * moveSpd * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CloudChecker")
        {
            Spawn();
        }
    }
    public void Spawn()
    {
        int idx = Random.Range(0, cloudPoints.Count);

        transform.position = cloudPoints[idx].position;
        
        float randomScale = Random.Range(0.5f, 1.0f); 
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        float randomRotationY = Random.Range(0f, 360f); 
        transform.rotation = Quaternion.Euler(0, randomRotationY, 0);

        int score = MainManager.Get().score;
        moveSpd = Random.Range(2000.0f, 5000.0f);
        moveSpd += score;

    }

    public static void SetCloudPointsGroup()
    {
        cloudPointsGroup = GameObject.FindGameObjectWithTag("CLOUD_SPAWN_POINT_GROUP")?.transform;

        formations = new Transform[cloudPointsGroup.childCount];
        
        for (int i = 0; i < formations.Length; i++)
        {
            formations[i] = cloudPointsGroup.GetChild(i)?.transform;
            Transform formation = formations[i];

            for (int j = 0; j < formation.childCount; j++)
            {   
                Transform child = formation.GetChild(j);
                cloudPoints.Add(child);
            }
        }

        
    }
}
