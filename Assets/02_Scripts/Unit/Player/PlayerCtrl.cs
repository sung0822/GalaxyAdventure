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
    IWeaponController weaponController;

    List<IItem> items = new List<IItem>();
    List<IWeapon> weapons = new List<IWeapon>();

    int currentWeaponIdx = 0;
    int currentItemIdx = 0;

    Vector3 moveDir { get; set; }
    public float currentLevel { get { return _currentLevel; } set { _currentLevel = value; } }
    public float currentExp { get { return _currentExp; } set { _currentExp = value; } }
    public float maxExp { get { return _maxExp; } set { _maxExp = value; } }

    //float IPlayer.level { get { return _level; } set { _level = value; } }
    //float IPlayer.currentExp { get { return _currentExp; } set { _currentExp = value; } }
    //float IPlayer.maxExp { get { return _maxExp; } set { _maxExp = value; } }


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
        weapons[currentItemIdx].user = this;
    }

    protected override void Update()
    {
        base.Update();
        
        HandleInput();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "Trigger 부딪힘!!");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
    }

    protected override void OnCollisionStay(Collision collision)
    {
    }

    void HandleInput()
    {
        inputHor = Input.GetAxisRaw("Horizontal");//Mathf.Floor(Input.GetAxis("Horizontal"));
        inputVer = Input.GetAxisRaw("Vertical");//MathF.Floor(Input.GetAxis("Vertical"));

        float a = Mathf.Floor(0.4f);
        float b = Mathf.Floor(0.9f);



        moveDir = ((Vector3.forward * inputVer) + (Vector3.right * inputHor)).normalized;
        if (moveDir == Vector3.zero)
        {
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            weapons[currentWeaponIdx].Use();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            items[currentItemIdx].Use();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentWeaponIdx == weapons.Count)
            {
                currentWeaponIdx = 0;
            }
            else
            {
                currentWeaponIdx++;
            }
        }

    }

    float inputHor;
    float inputVer;
    void Move()
    {
        rigidbody.velocity = moveDir * moveSpd;
    }

    public void GivePlayerExp(float exp)
    {
        currentExp += exp;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    protected void LevelUp()
    {
        currentLevel++;

        currentExp = 0;
        maxExp = maxExp * 1.5f;
        
        maxHp = maxHp + (int)(maxHp * 0.1f);
        currentHp = maxHp;

        power += (int)(power * 0.1f);
        
    }

    public override void Hit(int damage)
    {
        if(isImmortal)
        {
            return;
        }
        currentHp -= damage;
        isImmortal = true;
        immortalTime = 1;
        //rigidbody.isKinematic = true;
    }

}
