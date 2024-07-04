using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : Enemy
{
    public override bool isAttacking { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    RotatingPattern rotatingPattern;
    protected override void Start()
    {
        base.Start();

        rotatingPattern = new RotatingPattern();

        rotatingPattern.SetAxisOfRevolution(transform);

    }

    protected override void Update()
    {
        base.Update();
    }

    void setAxisOfRevolution()
    {
        rotatingPattern.SetAxisOfRevolution(transform);
    }

}
