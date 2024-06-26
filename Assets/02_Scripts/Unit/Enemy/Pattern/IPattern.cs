using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPattern
{
    Transform target{ get; }
    float timeElapsed { get; }
    
    void SetTarget(Transform target);
    string Name { get; set; }
    public void Execute();

}
