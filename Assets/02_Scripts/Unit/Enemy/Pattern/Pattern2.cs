using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2 : IPattern
{
    public Transform target { get { return _target; } set { _target = value; }}

    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }

    public float moveSpd { get { return _moveSpd; } set { _moveSpd = value; }}

    public bool isAdjustingSpd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    protected Transform _target;
    
    private Transform playerTransform;
    
    protected float _timeElapsed;
    float _moveSpd = 1;


    public void Execute()
    {
        timeElapsed = Time.deltaTime;

        Debug.Log("Pattern2 Ω««‡¡ﬂ");
        target.LookAt(playerTransform);

        target.transform.Translate(Vector3.forward * moveSpd * Time.deltaTime, Space.Self);

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
        throw new System.NotImplementedException();
    }
}
