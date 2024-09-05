using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : UnitBase, IMovalble, IAttackable
{
    
    protected IPlayer player;
    public override TeamType teamType { get { return _currentEnemyBaseData.teamType; } set { _currentEnemyBaseData.teamType = value; } }
    public bool isAttacking { get { return currentEnemyBaseData.isAttacking; } set { currentEnemyBaseData.isAttacking = value; } }
    public float moveSpd { get { return currentEnemyBaseData.moveSpd; } set { currentEnemyBaseData.moveSpd = value; } }
    public int power { get { return currentEnemyBaseData.power; } set { currentEnemyBaseData.power = value; } }
    public EnemyBaseData currentEnemyBaseData { get { return _currentEnemyBaseData; } set { _currentEnemyBaseData = value; } }

    protected EnemyBaseData _currentEnemyBaseData;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
    }

    Coroutine coroutineWaitAndReturnObject;
    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();

        _currentEnemyBaseData = (EnemyBaseData)currentUnitBaseData;

        player = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Player>();
        coroutineWaitAndReturnObject = StartCoroutine(WaitAndReturnObject());
    }
    IEnumerator WaitAndReturnObject()
    {
        yield return new WaitForSeconds(_currentEnemyBaseData.lifeTime);
        Debug.Log("lifeTime이 끝남");
        ObjectPoolManager.instance.ReturnObject(_currentEnemyBaseData.unitName + " Pool", this.gameObject);
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

        if (CheckCollideWall(other) && !currentEnemyBaseData.hasCollideWithWall)
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
        
        currentEnemyBaseData.hasCollideWithWall = true;
        StartCoroutine(AdjustSpeed(_currentEnemyBaseData.spdChanged, _currentEnemyBaseData.spdChangeDuration));
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
        StopCoroutine(coroutineWaitAndReturnObject); 
        player.GivePlayerExp(_currentEnemyBaseData.rewardExp);
        MainManager.instance.AddScore(_currentEnemyBaseData.rewardScore);
        UIManager.instance.CheckScore();
        player.GivePlayerAbilityGage(_currentEnemyBaseData.rewardAbilityGage);

        GameObject item = ItemManager.instance.MakeItem(transform.position);
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * 1 * moveSpd * Time.deltaTime, Space.Self);
    }
    

    /// <summary>
    /// �ӵ��� �������� �Ͽ� �����մϴ�. ������ �ð��� �Ű������� �޽��ϴ�.
    /// </summary>
    protected virtual IEnumerator AdjustSpeed(float spd, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalMoveSpd = moveSpd;
        _currentEnemyBaseData.enableAttack = true;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;


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
