using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float moveSpd = 10;


    void Start()
    {
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CloudChecker")
        {
            BackGroundManager.instance.SpawnCloud(gameObject);
        }
    }


    private void OnDestroy()
    {
    }

    static public void InitClouds()
    {
    }
}
