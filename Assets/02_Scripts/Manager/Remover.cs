using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PROJECTILE")
        {
            Destroy(other.transform.root.gameObject);
        }
        else if (other.transform.root.tag =="ENEMY")
        {
            Destroy(other.transform.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {

    }
}
