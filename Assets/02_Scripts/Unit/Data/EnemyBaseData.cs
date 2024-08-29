using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBaseData", menuName = "UnitData/UnitBaseData/EnemyBaseData", order = 1)]
public class EnemyBaseData : UnitBaseData
{
    public int rewardExp { get { return _rewardExp; } set { _rewardExp = value; } }
    [SerializeField] protected int _rewardExp;

    public int rewardScore { get { return _rewardScore; } }
    [SerializeField] protected int _rewardScore;

    public bool enableSlow { get { return _enableSlow; } set { _enableSlow = value; } }
    [SerializeField] protected bool _enableSlow = false;

    public bool enableAttack { get { return _enableAttack; } set { _enableAttack = value; } }
    [SerializeField] protected bool _enableAttack = false;

    public float lifeTime { get { return _lifeTime; } set { _lifeTime = value; } }
    [SerializeField] protected float _lifeTime = 0;

    public float spdChanged { get { return _spdChanged; } set { _spdChanged = value; } }
    [SerializeField] protected float _spdChanged;

    public float spdChangeDuration { get { return _spdChangeDuration; } set { _spdChangeDuration = value; } }
    [SerializeField] protected float _spdChangeDuration;

    public int power { get { return _power; } set { _power = value; } }
    [SerializeField] protected int _power;

    public float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField] protected float _moveSpd;

    public int rewardAbilityGage { get { return _rewardAbilityGage; } set { _rewardAbilityGage = value; } }
    [SerializeField] protected int _rewardAbilityGage;

}
