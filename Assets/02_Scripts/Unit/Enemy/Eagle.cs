using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Eagle : EnemyBase
{
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

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

    public override void DieUnit()
    {
        player.GivePlayerExp(rewardExp);
        MainManager.instance.AddScore(rewardScore);
        UIManager.instance.CheckScore();


        GameObject item = ItemManager.instance.MakeItem(transform);
        
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
