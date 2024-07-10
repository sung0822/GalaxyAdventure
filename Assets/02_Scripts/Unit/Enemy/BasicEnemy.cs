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
    float shootCycle = 3.0f;
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    bool _isAttacking;
    protected override float spdChanged { get { return _spdChanged; } set { _spdChanged = value; } }
    private float _spdChanged = 4.0f;
    protected override float spdChangeDuration { get { return _spdChangeDuration; } set { _spdChangeDuration = value; } }
    private float _spdChangeDuration = 0.75f;

    public override int power { get { return _power; } set { _power = value; } }
    [SerializeField] int _power = 10;
    public override int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField] int _maxHp = 40;
    public override int currentHp { get { return _currentHp; } set { _currentHp = value; } }
    [SerializeField] int _currentHp = 40;
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
        gunItemData = Instantiate(Resources.Load<GunItemData>("Datas/Weapons/BasicGunData"));

        gunItemData.SetStatus(10, 1, currentWeaponSpace, this, teamType, this);
        currentWeapon = new BasicGun(gunItemData);
        if (currentWeapon == null)
        {
            Debug.Log("¹«±â nullÀÓ");
        }
        
        weapons.Add(currentWeapon);

        currentWeapon = weapons[0];
        //currentWeapon.weaponSpace = weaponSpace;
        //currentWeapon.attackableUser = this;
        //currentWeapon.teamType = this.teamType;
        isAttacking = false;
    }


    protected override void Update()
    {
        base.Update();
        

        if (enableAttack && !isAttacking)
        {
            StartCoroutine(StartAttack());
            isAttacking = true;
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

    IEnumerator StartAttack()
    {
        while (true) 
        {
            Attack();
            yield return new WaitForSeconds(shootCycle);
        
        }
    }

}
