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
    ItemComponent showingConsumableItem;
    ItemComponent showingWeaponItem;

    TextMeshProUGUI consumableItemText;
    TextMeshProUGUI weaponItemText;

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

        consumableItemText = panel_Status.transform.Find("Panel_ConsumableItem").GetComponentInChildren<TextMeshProUGUI>();

        weaponItemText = panel_Status.transform.Find("Panel_ConsumableItem").GetComponentInChildren<TextMeshProUGUI>();

        items.AddRange(rtMaker.transform.GetComponentsInChildren<ItemComponent>());

        for (int i = 0; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(false);
        }
        CheckItem(ItemType.Consumable);
        CheckItem(ItemType.Weapon);

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

    public void CheckItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Consumable:
                ItemBase selectedConsumableItem = playerCtrl.selectedConsumableItem;
                if (selectedConsumableItem == null)
                {
                    return;
                }


                int count = playerCtrl.inventory.GetItemCount(selectedConsumableItem.data.id);
                Debug.Log("ConsumableItem Count: " + count);

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].itemId != playerCtrl.selectedConsumableItem.data.id)
                    {
                        continue;
                    }
                    // 해당 렌더텍스쳐와 플레이어 아이템 id가 같을 시
                    if (showingConsumableItem == null)
                    {
                        showingConsumableItem = items[i];
                        showingConsumableItem.gameObject.SetActive(true);
                        break;
                    }

                    showingConsumableItem.gameObject.SetActive(false);

                    showingConsumableItem = items[i];
                    showingConsumableItem.gameObject.SetActive(true);
                    break;
                }

                consumableItemText.text = "x" + count.ToString();

                break;
            case ItemType.Weapon:
                break;
            default:
                break;
        }
        
    }

    public void SwitchPausePanel(bool isPaused)
    {
        Panel_Pause.SetActive(isPaused);
    }


}
