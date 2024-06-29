using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStage : MonoBehaviour
{
    float timeElapsed { get; set; }
    Transform target { get; set; }

    public abstract void Execute();

    public abstract void SetUp();
}
