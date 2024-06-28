using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IStage
{
    float timeElapsed { get; set; }
    Transform target {  get; set; }

    void Execute();

    void SetUp();
}
