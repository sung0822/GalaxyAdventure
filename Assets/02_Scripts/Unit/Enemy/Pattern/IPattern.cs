using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPattern
{
    GameObject target{ get; }
    float timeElapsed { get; }
    
    void SetTarget(GameObject target);
    string Name { get; set; }
    public void Execute();

}
