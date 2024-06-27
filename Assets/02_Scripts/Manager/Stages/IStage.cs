using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IStage
{
    float timeElapsed { get; set; }
    IPlayer player { get; set; }

    void Execute();

}
