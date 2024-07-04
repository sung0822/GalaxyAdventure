using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPattern
{
    Transform target{ get; set; }

    /// <summary>
    /// ������ Ÿ���� �����̰��ϴ� �ӵ�. �⺻���� 1�Դϴ�
    /// </summary>
    float moveSpd{get; set;}
    bool isAdjustingSpd { get; set; }
    
    void SetTarget(Transform target);

    void ChangePattern(int idx);

    public void Execute();

    /// <summary>
    /// ���� �ӵ� ������ �����մϴ�. ������ �ð��� �Ű������� �޽��ϴ�.
    /// </summary>
    public void AdjustSpeed(float spdMultiplier, float duration);

}
