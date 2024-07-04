using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : Enemy
{
    public override bool isAttacking { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    protected override void Start()
    {
        base.Start();
        currentPattern = patterns[1];
    }

    protected override void Update()
    {
        base.Update();

        currentPattern.Execute();

    }

}
