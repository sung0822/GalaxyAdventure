using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance == this)
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }
    GameObject player;
    PlayerCtrl playerCtrl;

    float inputHor;
    float inputVer;

    void Start()
    {
        player = GameObject.FindWithTag("PLAYER");
        playerCtrl = player.GetComponent<PlayerCtrl>();
        
    }

    void Update()
    {
        OnPlaying();
    }

    void OnPlaying()
    {
        inputHor = Input.GetAxisRaw("Horizontal");//Mathf.Floor(Input.GetAxis("Horizontal"));
        inputVer = Input.GetAxisRaw("Vertical");//MathF.Floor(Input.GetAxis("Vertical"));

        float a = Mathf.Floor(0.4f);
        float b = Mathf.Floor(0.9f);

        Vector3 moveDir = ((Vector3.forward * inputVer) + (Vector3.right * inputHor)).normalized;

        playerCtrl.moveDir = ((Vector3.forward * inputVer) + (Vector3.right * inputHor)).normalized;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerCtrl.isAttacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            playerCtrl.isAttacking = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerCtrl.items[playerCtrl.currentItemIdx].Use();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerCtrl.currentWeaponIdx == playerCtrl.weapons.Count)
            {
                playerCtrl.currentWeaponIdx = 0;
            }
            else
            {
                playerCtrl.currentWeaponIdx++;
            }
        }

        ReceiveCheatKey();
    }

    void ReceiveCheatKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            playerCtrl.ChangeIsImmortal(!playerCtrl.isImmortal);
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            playerCtrl.LevelUp();
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            playerCtrl.LevelDown();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            playerCtrl.currentHp = playerCtrl.maxHp;
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {

        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {

        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {

        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            MainManager.instance.score += 100;
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {

        }
        else if (Input.GetKeyDown(KeyCode.P))
        {

        }
    }
    



}
