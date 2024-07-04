using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPattern : IPattern
{
    public Transform target { get { return _target; } set { _target = value; }}
    public Transform axisOfRevolution { get { return _axisOfRevolution; } set { _axisOfRevolution = value; } }
    private Transform _axisOfRevolution;

    public float moveSpd { get { return _moveSpd; } set { _moveSpd = value; }}
    public float rotateSpd { get { return _rotateSpd; } set { _rotateSpd = value; } }

    public bool isAdjustingSpd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    protected Transform _target;
    
    private Transform playerTransform;
    
    protected float _timeElapsed;
    float _moveSpd = 1;
    float _rotateSpd = 1;

    public void Execute()
    {
        target.LookAt(playerTransform);

        target.RotateAround(axisOfRevolution.position, Vector3.up, 1);

        target.transform.Translate(Vector3.forward * moveSpd * Time.deltaTime, Space.World);

    }

    public void SetTarget(Transform target)
    {
        _moveSpd = 3;
        this.target = target;
        playerTransform = GameObject.FindWithTag("PLAYER").transform;
    }

    public void ChangePattern(int idx)
    {

    }

    public void AdjustSpeed(float spdMultiplier, float duration)
    {

    }

    public void SetAxisOfRevolution(Transform axisOfRevolution)
    {
        this.axisOfRevolution = axisOfRevolution;
    }
}
