using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayEnemy : EnemyBase
{
    WeaponSpace currentWeaponSpace;

    protected WeaponItemBase currentWeapon;
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    bool _isAttacking;

    [SerializeField] GunItemData gunItemData;
    public override void Attack()
    {
        currentWeapon.Use();
    }

    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();

        currentWeaponSpace = transform.GetComponentInChildren<WeaponSpace>();
        gunItemData = ScriptableObject.Instantiate(gunItemData);


        gunItemData.power = 10;
        gunItemData.level = 1;
        gunItemData.useCycle = 1.7f;
        gunItemData.forceForProjectile = 8;
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
            Attack();
    }


}
