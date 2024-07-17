using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : UnitBase, IMovalble, IAttackable
{
    //protected Dictionary<string, IPattern> patternsDic = new Dictionary<string, IPattern>();

    protected IPlayer player;
    public override TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    protected TeamType _teamType = TeamType.ENEMY;

    public int rewardExp { get { return _rewardExp; } set { _rewardExp = value; } }
    [SerializeField] protected int _rewardExp;  
    public int rewardScore { get { return _rewardScore; } }
    [SerializeField] protected int _rewardScore;

    public bool enableSlow = false;
    public bool enableAttack = false;

    public float lifeTime = 0;
    public float spdChanged { get {return _spdChanged; } set { _spdChanged = value; } }
    [SerializeField] protected float _spdChanged;
    public float spdChangeDuration { get { return _spdChangeDuration; } set{ _spdChangeDuration = value; } }
    [SerializeField] protected float _spdChangeDuration;
    public int power { get { return _power; } set { _power = value; } }
    [SerializeField] protected int _power;

    public float moveSpd { get { return _moveSpd;} set { _moveSpd = value; } }
    [SerializeField] protected float _moveSpd;

    public float rewardAbilityGage { get { return _rewardAbilityGage; } set { _rewardAbilityGage = value; } }
    [SerializeField] protected float _rewardAbilityGage;

    protected bool hasCollideWithWall = false;

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Enemy의 기본 상태를 설정합니다. 속도, 체력, 공격력, 무기등은 구체화된 클래스에서 구현해야합니다.
    /// </summary>
    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();

        player = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Player>();
        transform.SetParent(MainManager.instance.mainStage.transform);
        SetImmortal(true);
        Destroy(gameObject, lifeTime);
    }


    protected override void Update()
    {
        base.Update();
        Move();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (CheckCollideWall(other) && !hasCollideWithWall)
        {
            HandleColliderWall();
        }
    }

    /// <summary>
    /// 벽 충돌 검사.
    /// </summary>
    protected bool CheckCollideWall(Collider other)
    {
        if (other.GetComponentInParent<Rigidbody>()?.transform.tag == "WALL")
            return true;
        else
            return false;
    }

    protected virtual void HandleColliderWall()
    {
        
        hasCollideWithWall = true;
        StartCoroutine(AdjustSpeed(spdChanged, spdChangeDuration));
        SetImmortal(false);
    }

    public override void Hit(int damage)
    {
        base.Hit(damage);
    }

    public override void Hit(int damage, Transform hitTransform)
    {
        base.Hit(damage, hitTransform);
    }

    public override void Hit(int damage, Vector3 position)
    {
        base.Hit(damage, position);
    }

    public override void DieUnit()
    {
        DieEnemy();
        base.DieUnit();
    }

    protected void DieEnemy()
    {
        player.GivePlayerExp(rewardExp);
        MainManager.instance.AddScore(rewardScore);
        UIManager.instance.CheckScore();
        player.GivePlayerAbilityGage(rewardAbilityGage);

        GameObject item = ItemManager.instance.MakeItem(transform.position);
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * 1 * moveSpd * Time.deltaTime, Space.Self);
    }
    

    /// <summary>
    /// 속도를 선형보간 하여 조절합니다. 보간할 시간을 매개변수로 받습니다.
    /// </summary>
    protected virtual IEnumerator AdjustSpeed(float spd, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalMoveSpd = moveSpd;
        enableAttack = true;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // 정규화한 길이.
            float normalizedTime = timeAdjustingSpd / duration;

            if (normalizedTime >= 1)
            {
                break;
            }

            moveSpd = Mathf.Lerp(originalMoveSpd, spd, normalizedTime);

            yield return new WaitForEndOfFrame();
        }
    }

    public abstract void Attack();

    public virtual void StopAttack()
    {
    }
}
