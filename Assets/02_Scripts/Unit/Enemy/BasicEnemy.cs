using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : Enemy
{
    Transform middleTransform;
    float time = 0;

    public override bool isAttacking 
    {
        get
        {
            return _isAttacking;
        } 
        set
        {
            _isAttacking = value;
        }
    }

    bool _isAttacking;

    protected override void Start()
    {
        base.Start();
        rewardExp = 10;
        rewardScore = 100;
        lifeTime = 0;

        middleTransform = transform.Find("Jet/Mesh/MiddlePosition");
        weapons.Add(middleTransform.AddComponent<BasicGun>());

        currentPattern.moveSpd = 10;

        weapons[0].user = this;

    }

    protected override void Update()
    {
        base.Update();

        time += Time.deltaTime;
        currentPattern.Execute();
        
        if (enableAttack)
        {
            Shoot();
        }

        if (lifeTime >= 20.0f)
        {
            Destroy(this.gameObject);
        }
    }


    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //if (other.transform.la) { }
    }

    void Shoot()
    {
        if (time > 1.0f)
        {
            weapons[0].Use();
            time = 0;
        }
    }
}
