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

    public TextMeshProUGUI scoreText;

    public PlayerCtrl playerCtrl;

    public GameObject Panel_Pause;
    void Start()
    {
        GameObject ui_Panel = GameObject.FindGameObjectWithTag("UI_PANEL");
        Transform hpPanel = ui_Panel.transform.Find("Panel_HpBar");
        Transform expPanel = ui_Panel.transform.Find("Panel_ExpBar");

        hpBar = hpPanel.Find("HpBar").GetComponent<Image>();
        hpText = hpPanel.Find("Hp_Text").GetComponent<TextMeshProUGUI>();

        expBar = expPanel.Find("ExpBar").GetComponent<Image>();
        expText = expPanel.Find("Exp_Text").GetComponent<TextMeshProUGUI>();

        //scoreText = ui_Panel.transform.Find("Panel_Score").Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        
        playerCtrl = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<PlayerCtrl>();
        
        hpText.text = playerCtrl.currentHp.ToString() + " / " + playerCtrl.maxHp.ToString();
        expText.text = playerCtrl.currentExp.ToString() + " / " + playerCtrl.maxExp.ToString();
        CheckScore();
        CheckPlayerHp();
        CheckPlayerExp();

        Panel_Pause = GameObject.Find("Panel_Pause");
        Panel_Pause.SetActive(false);



        //Panel_Pause = GameObject.FindGameObjectsWithTag("Panel_Pause")[1];

    }

    void Update()
    {

    }

    public void CheckPlayerHp()
    {
        hpText.text = playerCtrl.currentHp.ToString() + " / " + playerCtrl.maxHp.ToString();

        hpBar.fillAmount = (float)playerCtrl.currentHp / (float)playerCtrl.maxHp;
    }

    public void CheckPlayerExp()
    {
        expText.text = playerCtrl.currentExp.ToString() + " / " + playerCtrl.maxExp.ToString();
        
        expBar.fillAmount = (float)playerCtrl.currentExp / (float)playerCtrl.maxExp;
    }

    public void CheckScore()
    {
        scoreText.text = MainManager.instance.score.ToString();
    }

    public void SwitchPausePanel(bool isPaused)
    {
        Panel_Pause.SetActive(isPaused);
    }


}
