using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public TeamType teamType;
    private void OnTriggerEnter(Collider other)
    {
        Transform root = other.transform.root;
        if (root.tag == "PROJECTILE")
        {
            if (root.GetComponent<Projectile>().teamType != teamType)
            {
                Destroy(other.transform.root.gameObject);
            }
        }
    }
}
