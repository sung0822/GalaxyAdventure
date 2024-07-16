using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    [SerializeField] Color color;
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = this.color; 
        Gizmos.DrawSphere(transform.position, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
