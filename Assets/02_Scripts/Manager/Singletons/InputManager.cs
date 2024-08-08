using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    GameObject player;
    Player playerCtrl;

    float inputHor;
    float inputVer;

    void Start()
    {
        player = GameObject.FindWithTag("PLAYER");
        playerCtrl = player.GetComponent<Player>();
    }

    void Update()
    {
        OnPlaying();
        ReceiveCheatKey();
    }

    void OnPlaying()
    {
        inputHor = Input.GetAxisRaw("Horizontal");
        inputVer = Input.GetAxisRaw("Vertical");


        Vector3 moveDir = ((Vector3.forward * inputVer) + (Vector3.right * inputHor)).normalized;

        playerCtrl.moveDir = ((Vector3.forward * inputVer) + (Vector3.right * inputHor)).normalized;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerCtrl.isAttacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            playerCtrl.StopAttack();
        }
        else if(Input.GetKeyUp(KeyCode.Q))
        {
            playerCtrl.SpecialAttack();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            playerCtrl.UseItem(ItemType.Consumable);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playerCtrl.ChangeSelectedItem(ItemType.Consumable);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            playerCtrl.ChangeSelectedItem(ItemType.Weapon);
        }

    }

    void ReceiveCheatKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            playerCtrl.SetImmortalDuring(!playerCtrl.isImmortal, float.MaxValue);
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            playerCtrl.LevelUp();
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            playerCtrl.LevelDown();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            playerCtrl.currentHp = playerCtrl.maxHp;
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            ItemManager.instance.MakeItem(Vector3.zero, 1, 2);
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            ItemManager.instance.MakeItem(Vector3.zero, 3, 5);
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            MainManager.instance.AddScore(1000);
            UIManager.instance.CheckScore();
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            UIManager.instance.DisplayPlayerLevelUpTable();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {

        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            MainManager.instance.SwitchPauseStat();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            MainManager.instance.EndLevel();
        }
    }
    



}
