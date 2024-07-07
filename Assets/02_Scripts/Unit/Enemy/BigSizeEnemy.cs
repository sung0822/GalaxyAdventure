using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSizeEnemy : EnemyBase
{
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    private bool _isAttacking = false;
    protected override float spdChanged { get { return _spdChanged; } set { _spdChanged = value; } }
    private float _spdChanged = 2.0f;
    protected override float spdChangeDuration { get { return _spdChangeDuration; } set { _spdChangeDuration = value; } }
    private float _spdChangeDuration = 0.75f;

    public override int power { get { return _power; } set { _power = value; } }
    [SerializeField] int _power = 20;
    public override int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField] int _maxHp = 700;
    public override int currentHp { get { return _currentHp; } set { _currentHp = value; } }
    [SerializeField] int _currentHp = 700;
    public override float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField] float _moveSpd = 10;

    protected override int rewardExp { get { return _rewardExp; } set { _rewardExp = value; } }
    [SerializeField] int _rewardExp = 10;
    protected override int rewardScore { get { return _rewardScore; } set { _rewardScore = value; } }

    [SerializeField] protected int _rewardScore = 100;

    public override void Attack()
    {

    }
}
