using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Unit, IPlayer
{
    public GameObject camera;
    private Transform playerTransform;
    private MeshCollider meshCollider;

    List<IItem> items = new List<IItem>();
    List<IWeapon> weapons = new List<IWeapon>();

    IItem equippedItem = null;
    IWeapon equippedWeapon = null;
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
    }

    protected override void Update()
    {
        HandleInput();
        Move();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger ÇÃ·¹ÀÌ¾î ºÎµúÈû!!");

    }
    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision ÇÃ·¹ÀÌ¾î ºÎµúÈû!!");
    }


    void HandleInput()
    {
        inputHor = Input.GetAxis("Horizontal");
        inputVer = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            equippedWeapon.Use();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {

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
