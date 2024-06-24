using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform targetTr;  

    private Transform cameraTr;

    // Start is called before the first frame update
    void Start()
    {
        cameraTr = transform;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
