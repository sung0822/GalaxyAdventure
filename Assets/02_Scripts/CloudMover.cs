using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public static List<Transform> cloudPoints = new List<Transform>();

    public float spd = 10;
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
        cloudTransform.Translate(Vector3.forward * -1 * spd * Time.deltaTime, Space.World);
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
        
        // 크기 랜덤 설정
        float randomScale = Random.Range(0.5f, 1.0f); // 0.5배에서 2배 사이로 설정
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        // 회전 랜덤 설정
        float randomRotationY = Random.Range(0f, 360f); // Y축 기준으로 0도에서 360도 사이로 설정
        transform.rotation = Quaternion.Euler(0, randomRotationY, 0);

        spd = Random.Range(3.0f, 10.0f);

    }

    public static void SetCloudPointsGroup()
    {
        cloudPointsGroup = GameObject.FindGameObjectWithTag("CloudPointsGroup")?.transform;

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

        //foreach (Transform points in cloudPointsGroup)
        //{
        //    cloudPoints.Add(points);
        //}
        
    }
}
