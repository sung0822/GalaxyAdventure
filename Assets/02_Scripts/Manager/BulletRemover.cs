using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PROJECTILE"))
        {
            Debug.Log("�߻�ü �浹");
            Destroy(other.gameObject);
            return;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PROJECTILE"))
        {
            Debug.Log("�߻�ü �浹");
            Destroy(other.gameObject);
            return;
        }
    }
}
