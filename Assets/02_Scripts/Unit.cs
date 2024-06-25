using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected int maxHp;
    protected int currentHp;
    protected int power;

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

    public virtual void Hit(int damage)
    {
        currentHp -= damage;
    }





}
