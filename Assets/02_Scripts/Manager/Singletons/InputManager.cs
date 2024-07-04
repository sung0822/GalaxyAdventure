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
        else if(Input.GetKeyUp(KeyCode.Q))
        {
            playerCtrl.SpecialAttack();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            //playerCtrl.items[playerCtrl.currentItemIdx].Use();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playerCtrl.ChangeSelectedItem();
        }

    }

    void ReceiveCheatKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("ÀÔ·ÂµÆÀ½");
            playerCtrl.SetImmortal(!playerCtrl.isImmortal, float.MaxValue);
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
            MainManager.instance.SwitchPauseStat();
        }
    }
    



}
