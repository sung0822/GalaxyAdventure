using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "UnitData/UnitBaseData/BossData", order = 1)]
public class BossData : EnemyBaseData
{
    [Header("BossData")]
    
    [SerializeField] Color _page2Color;
    public Color page2Color { get { return _page2Color; } set { _page2Color = value; } }

    [SerializeField] Color _page3Color;
    public Color page3Color { get { return _page3Color; } set { _page3Color = value; } }
    
    [SerializeField] int _pageNumber;
    public int pageNumber { get { return _pageNumber; } set{ _pageNumber = value;} }
    
    public override void SetData(UnitBaseData unitBaseData)
    {
        base.SetData(unitBaseData);

        BossData bossData = (BossData)unitBaseData;


    }
}
