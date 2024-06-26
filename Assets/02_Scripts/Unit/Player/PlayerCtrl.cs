using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : Unit, IPlayer
{
    public GameObject camera;
    private Transform playerTransform;
    public Rigidbody rigidbody;

    Transform middleTransform;
    IWeaponController weaponController;

    List<IItem> items = new List<IItem>();
    List<IWeapon> weapons = new List<IWeapon>();

    int currentWeaponIdx = 0;
    int currentItemIdx = 0;

    Vector3 moveDir { get; set; }
    float IPlayer.level { get { return _level; } set { _level = value; } }
    float IPlayer.currentExp { get { return _currentExp; } set { _currentExp = value; } }
    float IPlayer.maxExp { get { return _maxExp; } set { _maxExp = value; } }


    float _level = 1;
    float _currentExp = 0;
    float _maxExp = 100;

    float moveSpd = 10;

    protected override void Start()
    {
        base.Start();

        playerTransform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();

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
        Debug.Log("Trigger 부딪힘!!");
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
            Debug.Log("멈춤");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(currentWeaponIdx);
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
        //Debug.Log(Time.fixedDeltaTime);
        //rigidbody.MovePosition(rigidbody.position + moveDir * moveSpd * Time.fixedDeltaTime);
    }

    void StopMove()
    {
        rigidbody.velocity = Vector3.zero;
    }

}
