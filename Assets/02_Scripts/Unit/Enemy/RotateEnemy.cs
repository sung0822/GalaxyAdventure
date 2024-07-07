using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : EnemyBase
{
    public override bool isAttacking { get; set; }
    protected override float spdChanged { get { return _spdChanged; } set { _spdChanged = value; } }
    protected override float spdChangeDuration { get { return _spdChangeDuration; } set { _spdChangeDuration = value; } }

    public override int power { get { return _power; } set { _power = value; } }
    [SerializeField] int _power = 10;
    public override int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField] int _maxHp = 40;
    public override int currentHp { get { return _currentHp; } set { _currentHp = value; } }
    [SerializeField] int _currentHp = 40;
    public override float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField] float _moveSpd = 10;

    protected override int rewardExp { get { return _rewardExp; } set { _rewardExp = value; } }
    [SerializeField] int _rewardExp = 10;
    protected override int rewardScore { get { return _rewardScore; } set { _rewardScore = value; } }
    [SerializeField] protected int _rewardScore = 100;

    private float _spdChanged = 4.0f;
    private float _spdChangeDuration = 0.75f;

    protected override void Start()
    {
        base.Start();

    }

    protected override void SetFirstStatus()
    {

    }
    protected override void Update()
    {
        base.Update();
    }

    void setAxisOfRevolution()
    {
    }

    public override void Attack()
    {
    }

}
