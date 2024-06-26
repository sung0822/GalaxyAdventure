using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currentHp;
    public int power { get { return _power; } set { _power = value; } }
    int _power;

    protected virtual void Start()
    {
        _power = 10;
        maxHp = 100;
        currentHp = 100;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void OnCollisionEnter(Collision other)
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }
    protected virtual void OnCollisionStay(Collision collision)
    {
        
    }

    public virtual void Hit(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("�ĸ���!");
    }

    protected virtual void LateUpdate()
    {
    }




}
