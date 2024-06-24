using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject camera;
    private Transform playerTransform;
    private MeshCollider meshCollider;

    Vector3 moveDir { get; set; }

    float level = 1;
    float currentHp = 100;
    float maxHp = 100;
    float power = 10;
    float currentExp = 0;
    float maxExp = 100;

    float spd = 10;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger ÇÃ·¹ÀÌ¾î ºÎµúÈû!!");

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision ÇÃ·¹ÀÌ¾î ºÎµúÈû!!");
    }


    void Move()
    {
        float inputHor = Input.GetAxis("Horizontal");
        float inputVer = Input.GetAxis("Vertical");

        moveDir = ((Vector3.forward * inputVer) + (Vector3.right * inputHor)).normalized;

        playerTransform.Translate(moveDir * spd * Time.deltaTime);
    }
}
