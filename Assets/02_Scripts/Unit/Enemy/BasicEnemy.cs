using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : Enemy
{
    Transform middleTransform;
    float time = 0;
    protected override void Start()
    {
        base.Start();
        rewardExp = 10;
        rewardScore = 100;

        middleTransform = transform.Find("Jet/Mesh/MiddlePosition");
        weapons.Add(middleTransform.AddComponent<BasicGun>());

        weapons[0].user = this;
    }

    protected override void Update()
    {
        base.Update();
        currentPattern.Execute();

        time += Time.deltaTime;
        if (time > 3.0f)
        {
            weapons[0].Use();
            time = 0;
        }
    }


    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }


}
