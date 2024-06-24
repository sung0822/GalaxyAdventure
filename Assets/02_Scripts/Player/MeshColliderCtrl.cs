using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerºÎµúÈû!");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CollsionºÎµúÈû!");
    }


    // Update is called once per frame
    void Update()
    {
    }
}
