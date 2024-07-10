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
    /// Enemy�� �⺻ ���¸� �����մϴ�. �ӵ�, ü��, ���ݷ�, ������� ��üȭ�� Ŭ�������� �����ؾ��մϴ�.
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
    /// �� �浹 �˻�.
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
    /// �ӵ��� �������� �Ͽ� �����մϴ�. ������ �ð��� �Ű������� �޽��ϴ�.
    /// </summary>
    protected IEnumerator AdjustSpeed(float spd, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalMoveSpd = moveSpd;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // ����ȭ�� ����.
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
