using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : Unit, IPlayer
{
    private Transform playerTransform;
    public Rigidbody rigidbody;

    Transform middleTransform;
    public Inventory inventory { get { return _inventory; }}
    protected Inventory _inventory = new Inventory();

    public List<WeaponBase> weapons = new List<WeaponBase>();

    public ItemBase selectedItem;
    
    public int currentWeaponIdx = 0;

    /// <summary>
    /// 적재된 순서대로 아이템 순서가 생김. currentItemIdx로 접근 
    /// </summary>
    Dictionary<int, int> itemOder = new Dictionary<int, int>();

    protected int currentItemIdx = -1;


    public override bool isAttacking {
        get
        {
            return _isAttacking;
        }
        set
        {
            _isAttacking = value;
        }
    }
    protected bool _isAttacking;
     
    public Vector3 moveDir { get { return _moveDir; } set { _moveDir = value; } }
    Vector3 _moveDir;
    public float currentLevel { get { return _currentLevel; } set { _currentLevel = value; } }
    public float currentExp { get { return _currentExp; } set { _currentExp = value; } }
    public float maxExp { get { return _maxExp; } set { _maxExp = value; } }


    protected float previousMaxExp;
    protected int previousMaxHp;

    [SerializeField] float _currentLevel = 1;
    [SerializeField] float _currentExp = 0;
    [SerializeField] float _maxExp = 100;

    float moveSpd = 10;

    protected override void Start()
    {
        base.Start();
        _myTeam = TeamType.ALLY;

        playerTransform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();


        // 중간 포지션에 무기 장착
        middleTransform = playerTransform.Find("Jet/Mesh/MiddlePosition");
        middleTransform.AddComponent<BasicGun>();
        
        weapons.Add(middleTransform.GetComponent<BasicGun>());
        weapons[currentWeaponIdx].shootCycle = 0.2f;
        weapons[currentWeaponIdx].user = this;

    }

    protected override void Update()
    {
        base.Update();
        Attack();
        
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    protected override void OnTriggerEnter(Collider other)
    {

    }
    protected override void OnCollisionEnter(Collision collision)
    {

    }

    protected override void OnCollisionStay(Collision collision)
    {
    }


    void Move()
    {
        rigidbody.velocity = moveDir * moveSpd;
    }

    public void GivePlayerExp(float exp)
    {
        currentExp += exp;
        UIManager.instance.CheckPlayerExp();

        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentLevel++;

        currentExp = 0;

        previousMaxExp = maxExp;
        maxExp = maxExp * 1.5f;

        previousMaxHp = maxHp;

        maxHp = maxHp + (int)(maxHp * 0.1f);
        currentHp = maxHp;

        power += (int)(power * 0.1f);

        UIManager.instance.CheckPlayerHp();
        UIManager.instance.CheckPlayerExp();
    }

    public void LevelDown()
    {
        currentLevel--;

        maxExp = previousMaxExp;
        maxHp = previousMaxHp;

        power += (int)(power * 0.1f);

        UIManager.instance.CheckPlayerHp();
        UIManager.instance.CheckPlayerExp();
    }

    public override void Hit(int damage)
    {
        if (isImmortal)
        {
            Debug.Log("무적이여서 안 맞음");
            return;
        }
        currentHp -= damage;
        CheckDead();
        
        isImmortal = true;
        immortalTime = 3.0f;
        UIManager.instance.CheckPlayerHp();
    }

    protected void Attack()
    {
        if(_isAttacking)
        {
            weapons[currentWeaponIdx].Use();
        }
    }

    public void ChangeSelcetedItem()
    {
        currentItemIdx += 1;
        Debug.Log("currentItemIdx: " + currentItemIdx);
        if (currentItemIdx == itemOder.Count)
        {
            Debug.Log("처음 인덱스로");
            currentItemIdx = 0;
        }
        selectedItem = inventory.GetItem(itemOder[currentItemIdx]);
        
        UIManager.instance.CheckItem();
    }

    public void UseItem()
    {
        selectedItem.Use();
    }

    public void GivePlayerItem(ItemBase item)
    {
        if(inventory.CheckExist(item))
        {
            inventory.Add(item);
        }
        else
        {
            inventory.Add(item);
            currentItemIdx++;

            itemOder.Add(itemOder.Count, item.id);
            selectedItem = item;
            Debug.Log("선택된 아이템 id: "+ selectedItem.id + "번");
        }

        UIManager.instance.CheckItem();
    }
}
