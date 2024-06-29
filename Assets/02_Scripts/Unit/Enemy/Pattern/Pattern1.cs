using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pattern1 : IPattern
{

    public Transform target { get { return _target; } set { _target = value; }}

    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }

    public float moveSpd 
    { 
    get { return _moveSpd; } 
    set {
            _moveSpd = value;
            _originalMoveSpd = value;
        }
    }

    public bool isAdjustingSpd { get { return _isAdjustingSpd; } set { _isAdjustingSpd = value; } }

    private bool _isAdjustingSpd;
    private float timeAdjustingSpd;
    
    protected Transform _target;
    private float _timeElapsed;

    private float _moveSpd = 1;
    private float _originalMoveSpd = 1;

    private float spdMultiplier;
    private float duration;

    public void AdjustSpeed(float spd, float duration)
    {
        if (duration <= 0)
        {
            Debug.LogWarning("duration must be bigger than 0");
            return;
        }
        isAdjustingSpd = true;
        this.spdMultiplier = spd;
        this.duration = duration;
    }

    public void Execute()
    {
        timeElapsed = Time.deltaTime;
        target.Translate(Vector3.forward * 1 * moveSpd * Time.deltaTime, Space.Self);

        if (isAdjustingSpd == true) 
        {
            LerpSpd();
        }
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ChangePattern(int idx)
    {

    }

    void LerpSpd()
    {

        timeAdjustingSpd += Time.deltaTime;
        //Debug.Log("경과시간: " + timeAdjustingSpd);
        //Debug.Log("지속시간: " + duration);

        float tmp = timeAdjustingSpd / duration;

        if (timeAdjustingSpd >= duration)
        {
            Debug.LogWarning("지속시간 다 됨");
            isAdjustingSpd = false;
            timeAdjustingSpd = 0;
            duration = 0;
            _originalMoveSpd = _moveSpd;
            return;
        }

        _moveSpd = Mathf.Lerp(_originalMoveSpd, spdMultiplier, tmp);

    }

    
}
