using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Eagle : EnemyBase
{

    private bool _isAttacking;

    [SerializeField] Transform targetPlayer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();
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
        base.DieUnit();
    }

    public override void Move()
    {
        transform.Translate(Vector3.forward * moveSpd * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// �ڻ캴�̱⶧���� �׳� �ٶ󺸰� �����ϴ� ����ü�� �����̶�� ������
    /// </summary>
    public override void Attack()
    {
        transform.LookAt(targetPlayer);
        Move();
    }

}
