using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class BasicEnemy : EnemyBase
{
    WeaponSpace currentWeaponSpace;

    protected WeaponItemBase currentWeapon;
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    bool _isAttacking;
    protected override float spdChanged { get { return _spdChanged; } set { _spdChanged = value; } }
    private float _spdChanged = 4.0f;
    protected override float spdChangeDuration { get { return _spdChangeDuration; } set { _spdChangeDuration = value; } }
    private float _spdChangeDuration = 0.75f;
    public override int power { get { return _power; } set { _power = value; } }
    [SerializeField] int _power = 10;
    public override float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField] float _moveSpd = 10;

    protected override int rewardExp { get { return _rewardExp; } set { _rewardExp = value; } }
    [SerializeField] int _rewardExp = 10;
    protected override int rewardScore { get { return _rewardScore; } set { _rewardScore = value; } }
    [SerializeField] protected int _rewardScore = 100;

    [SerializeField] GunItemData gunItemData;

    protected override void Start()
    {
        base.Start();
    }
    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();

        lifeTime = 0;

        currentWeaponSpace = transform.GetComponentInChildren<WeaponSpace>();
        gunItemData = ScriptableObject.Instantiate(gunItemData);


        gunItemData.power = 10;
        gunItemData.level = 1;
        gunItemData.weaponSpaceTransform = currentWeaponSpace.transform;
        gunItemData.unitUser = this;
        gunItemData.attackableUser = this;
        gunItemData.teamType = teamType;

        currentWeapon = (WeaponItemBase)gunItemData.CreateItem();

        
    }


    protected override void Update()
    {
        base.Update();

        if (enableAttack)
        {
            Attack();
        }
            
    }


    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void Attack()
    {
        currentWeapon.Use();
    }


}
