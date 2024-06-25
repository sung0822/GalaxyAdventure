using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{

    protected virtual void Start()
    {
            
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnCollisionEnter(Collision other)
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }

    public abstract void Use();

}
