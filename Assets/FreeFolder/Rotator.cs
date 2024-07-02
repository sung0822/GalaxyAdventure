using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float spd = 10;
    void Start()
    {
        
    }

    void Update()
    {
        transform.RotateAround(new Vector3(10, 0, 0), Vector3.up, 10 * spd * Time.deltaTime);
    }
}
