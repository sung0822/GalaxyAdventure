using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : Unit, IMovalble
{

    protected List<WeaponBase> weapons = new List<WeaponBase>();
    //protected Dictionary<string, IPattern> patternsDic = new Dictionary<string, IPattern>();
    

    protected IPlayer player;

    protected int rewardExp;
    protected int rewardScore;

    public bool enableSlow = false;
    public bool enableAttack = false;

    public float lifeTime;

    private bool isAdjustingSpd;
    private float timeAdjustingSpd;
    public float moveSpd { get; set; }

    private float _moveSpd = 1;
    private float _originalMoveSpd = 1;

    private float spdMultiplier;
    private float duration;


    protected override void Start()
    {
        base.Start();
        _myTeam = TeamType.ENEMY;

        weapons.Add(new BasicGun());

        moveSpd = 10;

        player = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Player>();
        transform.SetParent(null);
        SetImmortal(true);

    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        //enableSlow가 true라면 벽 충돌시 느려짐
        if (other.GetComponentInParent<Rigidbody>()?.transform.tag == "WALL")
        {
            enableAttack = true;
            SetImmortal(false);

            Debug.Log("Enemy isImmortal: " + isImmortal);
            if (!enableSlow)
            {
                return;
            }
            
        }
    }

    public override void Hit(int damage)
    {
        base.Hit(damage);
    }

    protected override void Die()
    {
        player.GivePlayerExp(rewardExp);
        MainManager.Get().score += rewardScore;
        UIManager.instance.CheckScore();

        GameObject item = ItemManager.instance.MakeItem(transform);
        item.GetComponent<ItemComponent>().transform.Rotate(-50, 0, 0);

        base.Die();

    }

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * 1 * moveSpd * Time.deltaTime, Space.Self);

        if (isAdjustingSpd == true)
        {
            LerpSpd();
        }
    }

    void AdjustSpeed(float spd, float duration)
    {
        if (duration <= 0)
        {
            Debug.LogWarning("duration must be bigger than 0");
            return;
        }
        isAdjustingSpd = true;
        this.spdMultiplier = spd;
        this.duration = duration;

    }
    void LerpSpd()
    {

        timeAdjustingSpd += Time.deltaTime;

        // 정규화한 길이.
        float tmp = timeAdjustingSpd / duration;

        if (timeAdjustingSpd >= duration)
        {
            isAdjustingSpd = false;
            timeAdjustingSpd = 0;
            duration = 0;
            _originalMoveSpd = _moveSpd;
            return;
        }

        _moveSpd = Mathf.Lerp(_originalMoveSpd, spdMultiplier, tmp);

    }

}
