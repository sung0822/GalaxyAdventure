using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : UnitBase, IMovalble, IAttackable
{
    protected List<WeaponItemBase> weapons = new List<WeaponItemBase>();
    protected WeaponItemBase currentWeapon;
    //protected Dictionary<string, IPattern> patternsDic = new Dictionary<string, IPattern>();

    protected IPlayer player;
    public override TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    protected TeamType _teamType = TeamType.ENEMY;

    protected abstract int rewardExp { get; set; }
    protected abstract int rewardScore { get; set; }

    public bool enableSlow = false;
    public bool enableAttack = false;

    public float lifeTime;
    protected abstract float spdChanged { get; set; }
    protected abstract float spdChangeDuration { get; set; }
    public abstract int power { get; set; }
    public abstract float moveSpd { get; set; }

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

        enableAttack = false;
        teamType = TeamType.ENEMY;
        player = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Player>();
        transform.SetParent(null);
        SetImmortal(true);
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
        enableAttack = true;
        hasCollideWithWall = true;
        StartCoroutine(AdjustSpeed(spdChanged, spdChangeDuration));
        SetImmortal(false);
    }

    protected override void DieUnit()
    {
        DieEnemy();
        base.DieUnit();
    }

    protected void DieEnemy()
    {
        player.GivePlayerExp(rewardExp);
        MainManager.Get().score += rewardScore;
        UIManager.instance.CheckScore();

        GameObject item = ItemManager.instance.MakeItem(transform);
        item.GetComponent<ItemComponent>().transform.Rotate(-50, 0, 0);
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * 1 * moveSpd * Time.deltaTime, Space.Self);
    }
    

    /// <summary>
    /// 속도를 선형보간 하여 조절합니다. 보간할 시간을 매개변수로 받습니다.
    /// </summary>
    protected IEnumerator AdjustSpeed(float spd, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalMoveSpd = moveSpd;

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

}
