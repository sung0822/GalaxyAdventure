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

    public Player playerCtrl;

    public GameObject Panel_Pause;

    public GameObject rtMaker;

    List<ItemComponent> items = new List<ItemComponent>();
    ItemComponent showingItem;

    TextMeshProUGUI itemCountText;

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");

        GameObject panel_Status = GameObject.FindGameObjectWithTag("UI_PANEL");
        Transform hpPanel = panel_Status.transform.Find("Panel_HpBar");
        Transform expPanel = panel_Status.transform.Find("Panel_ExpBar");

        hpBar = hpPanel.Find("HpBar").GetComponent<Image>();
        hpText = hpPanel.Find("Hp_Text").GetComponent<TextMeshProUGUI>();

        expBar = expPanel.Find("ExpBar").GetComponent<Image>();
        expText = expPanel.Find("Exp_Text").GetComponent<TextMeshProUGUI>();

        //scoreText = ui_Panel.transform.Find("Panel_Score").Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText = panel_Status.transform.Find("Panel_Score").Find("ScoreText").GetComponent<TextMeshProUGUI>();
        
        playerCtrl = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Player>();
        
        hpText.text = playerCtrl.currentHp.ToString() + " / " + playerCtrl.maxHp.ToString();
        expText.text = playerCtrl.currentExp.ToString() + " / " + playerCtrl.maxExp.ToString();
        CheckScore();
        CheckPlayerHp();
        CheckPlayerExp();

        Panel_Pause = canvas.transform.Find("Panel_Pause").gameObject;
        Panel_Pause.SetActive(false);


        rtMaker = GameObject.FindGameObjectWithTag("RT_MAKER"); 

        itemCountText = panel_Status.transform.Find("Panel_Item").GetComponentInChildren<TextMeshProUGUI>();

        items.AddRange(rtMaker.transform.GetComponentsInChildren<ItemComponent>());

        for (int i = 0; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(false);
        }
        CheckItem();

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

    public void CheckItem()
    {
        ItemBase selectedItem = playerCtrl.selectedItem;
        if (selectedItem == null)
        {
            return;
        }


        int count = playerCtrl.inventory.GetItemCount(selectedItem);

        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].itemId != playerCtrl.selectedItem.id)
            {
                continue;
            }
            // 해당 렌더텍스쳐와 플레이어 아이템 id가 같을 시
            if (showingItem == null)
            {
                showingItem = items[i];
                showingItem.gameObject.SetActive(true);
                break;
            }

            showingItem.gameObject.SetActive(false);

            showingItem = items[i];
            showingItem.gameObject.SetActive(true);
            break;
        }
        
        itemCountText.text = "x" + count.ToString();

    }

    public void SwitchPausePanel(bool isPaused)
    {
        Panel_Pause.SetActive(isPaused);
    }


}
