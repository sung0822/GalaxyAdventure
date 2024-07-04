using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPattern
{
    Transform target{ get; set; }

    /// <summary>
    /// 설정한 타겟을 움직이게하는 속도. 기본값은 1입니다
    /// </summary>
    float moveSpd{get; set;}
    bool isAdjustingSpd { get; set; }
    
    void SetTarget(Transform target);

    void ChangePattern(int idx);

    public void Execute();

    /// <summary>
    /// 곱한 속도 값으로 보간합니다. 보간할 시간을 매개변수로 받습니다.
    /// </summary>
    public void AdjustSpeed(float spdMultiplier, float duration);

}
