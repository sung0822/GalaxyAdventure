using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance = null;

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

    public Image hpBar;
    public TextMeshProUGUI hpText;
    
    public Image expBar;
    public TextMeshProUGUI expText;

    public PlayerCtrl playerCtrl;
    void Start()
    {
        GameObject ui_Panel = GameObject.FindGameObjectWithTag("UI_PANEL");
        Transform hpPanel = ui_Panel.transform.Find("Panel_HpBar");
        Transform expPanel = ui_Panel.transform.Find("Panel_ExpBar");

        hpBar = hpPanel.Find("HpBar").GetComponent<Image>();
        hpText = hpPanel.Find("Hp_Text").GetComponent<TextMeshProUGUI>();

        expBar = expPanel.Find("ExpBar").GetComponent<Image>();
        expText = expPanel.Find("Exp_Text").GetComponent<TextMeshProUGUI>();


        GameObject player = GameObject.FindGameObjectWithTag("PLAYER");
        playerCtrl = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<PlayerCtrl>();
        
        hpText.text = playerCtrl.currentHp.ToString() + " / " + playerCtrl.maxHp.ToString();
        expText.text = playerCtrl.currentExp.ToString() + " / " + playerCtrl.maxExp.ToString();
    }

    void Update()
    {

    }

    public void CheckPlayerHp()
    {
        hpText.text = playerCtrl.currentHp.ToString() + " / " + playerCtrl.maxHp.ToString();
    }

    public void CheckPlayerExp()
    {
        expText.text = playerCtrl.currentExp.ToString() + " / " + playerCtrl.maxExp.ToString();
    }

}
