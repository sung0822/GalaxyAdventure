using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : IPattern
{

    public Transform target { get { return _target; } set { _target = value; }}

    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }

    public float moveSpd { get { return _moveSpd; } set { _moveSpd = value; }}

    protected Transform _target;
    float _timeElapsed;
    bool isExecuting = false;
    float _moveSpd = 1;

    public void Execute()
    {
        timeElapsed = Time.deltaTime;
        target.Translate(Vector3.forward * -1 * moveSpd * Time.deltaTime, Space.World);
        
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
}
