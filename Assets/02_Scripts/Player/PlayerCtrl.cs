using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : Unit, IPlayer
{
    public GameObject camera;
    private Transform playerTransform;

    Transform middleTransform;

    List<IItem> items = new List<IItem>();
    List<BaseWeapon> weapons = new List<BaseWeapon>();

    int currentWeaponIdx = 0;
    int currentItemIdx = 0;

    Vector3 moveDir { get; set; }
    float IPlayer.level { get { return level; } set { level = value; } }
    float IPlayer.currentExp { get { return currentExp; } set { currentExp = value; } }
    float IPlayer.maxExp { get { return maxExp; } set { maxExp = value; } }

    float level = 1;
    float currentExp = 0;
    float maxExp = 100;

    float spd = 10;

    protected override void Start()
    {
        playerTransform = GetComponent<Transform>();
        middleTransform = playerTransform.Find("MiddlePosition");

        middleTransform.AddComponent<BasicGun>();
        BasicGun basicGun = middleTransform.GetComponent<BasicGun>();
        weapons.Add(basicGun);

    }

    protected override void Update()
    {
        HandleInput();
        Move();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger 부딪힘!!");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision 부딪힘!!");
    }


    void HandleInput()
    {
        inputHor = Input.GetAxis("Horizontal");
        inputVer = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            weapons[currentWeaponIdx].Use();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {

        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            items[currentItemIdx].Use();
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            currentWeaponIdx++;
        }
        




    }
    float inputHor;
    float inputVer;
    void Move()
    {
        
        moveDir = ((Vector3.forward * inputVer) + (Vector3.right * inputHor)).normalized;

        playerTransform.Translate(moveDir * spd * Time.deltaTime);
    }



}
