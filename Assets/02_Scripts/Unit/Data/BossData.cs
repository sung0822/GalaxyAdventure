using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "UnitData/UnitBaseData/BossData", order = 1)]
public class BossData : EnemyBaseData
{
    [Header("BossData")]
    [SerializeField] int a;
    public override void SetData(UnitBaseData unitBaseData)
    {
        base.SetData(unitBaseData);

        BossData bossData = (BossData)unitBaseData;


    }
}
