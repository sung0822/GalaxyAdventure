using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPattern
{
    Transform target{ get; set; }
    float timeElapsed { get; set; }

    float moveSpd{get; set;}
    
    void SetTarget(Transform target);

    void ChangePattern(int idx);

    public void Execute();

}
