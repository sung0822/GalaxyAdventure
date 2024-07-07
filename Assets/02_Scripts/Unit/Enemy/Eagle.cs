using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Eagle : EnemyBase
{
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

    protected override float spdChanged { get { return _spdChanged; } set { _spdChanged = value; } }
    protected override float spdChangeDuration { get { return _spdChangeDuration; } set { _spdChangeDuration = value; } }
    public override int power { get { return _power; } set { _power = value; } }
    [SerializeField] int _power = 10;
    public override int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField] int _maxHp = 10;
    public override int currentHp { get { return _currentHp; } set { _currentHp = value; } }
    [SerializeField] int _currentHp = 10;
    public override float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField] float _moveSpd = 10;

    protected override int rewardExp { get { return _rewardExp; } set { _rewardExp = value; } }
    [SerializeField] int _rewardExp = 10;
    protected override int rewardScore { get { return _rewardScore; } set { _rewardScore = value; } }
    [SerializeField] protected int _rewardScore = 50;

    private float _spdChanged = 4.0f;
    private float _spdChangeDuration = 0.75f;

    private bool _isAttacking;

    Transform targetPlayer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();
        lifeTime = 0;

        transform.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        targetPlayer = GameObject.FindWithTag("PLAYER").transform;
    }
    protected override void Update()
    {
        Attack();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void Die()
    {
        player.GivePlayerExp(rewardExp);
        MainManager.Get().score += rewardScore;
        UIManager.instance.CheckScore();


        GameObject item = ItemManager.instance.MakeItem(transform);
        
        item.GetComponent<ItemComponent>().transform.Rotate(-50, 0, 0);
        
        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.eagleDieParticle, transform.position, transform.rotation);

        particle.transform.localScale = this.transform.localScale * 0.1f;
        Destroy(particle, 1.5f);

        Destroy(this.gameObject);
    }

    public override void Move()
    {
        transform.Translate(Vector3.forward * moveSpd * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// 자살병이기때문에 그냥 바라보고 돌격하는 그자체로 공격이라고 간주함
    /// </summary>
    public override void Attack()
    {
        transform.LookAt(targetPlayer);
        Move();
    }

}
