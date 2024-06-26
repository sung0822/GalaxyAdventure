using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : MonoBehaviour, IPattern
{

    public string Name { get; set; }

    public GameObject target { get { return _target; } set { _target = value; }}

    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }

    protected GameObject _target;
    float _timeElapsed;

    public void Execute()
    {

    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    protected void Update()
    {
        
    }


}
