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

    [SerializeField] GunItemData gunItemData;

    protected override void Start()
    {
        base.Start();
    }
    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();

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

    public override void DieUnit()
    {
        base.DieUnit();
        _isAttacking = unitBaseData.isAttacking;
        
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
