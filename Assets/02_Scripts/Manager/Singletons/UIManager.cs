using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{

    public Image hpBar;
    public TextMeshProUGUI hpText;
    
    public Image expBar;
    public TextMeshProUGUI expText;

    public TextMeshProUGUI scoreText;

    public Image AbilityGageBar;
    public TextMeshProUGUI AbilityGageText;

    public Player playerCtrl;

    public GameObject panel_Pause;

    public GameObject rtMaker;
    public GameObject rtMaker_1;

    List<ItemComponent> consumableItems = new List<ItemComponent>();
    public ItemComponent showingConsumableItem;
    public TextMeshProUGUI consumableItemText;

    List<ItemComponent> weaponItems = new List<ItemComponent>();
    public ItemComponent showingWeaponItem;
    public TextMeshProUGUI weaponItemText;

    public TextMeshProUGUI finalScore;
    public GameObject panel_Final;
    public Button replayButton;

    public GameObject PanelLevel;

    public GridLayoutGroup PlayerLevelDataTable;
    public GameObject dataTextPrefab;
    public GameObject cell;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI currentExpText;

    void Start()
    {
        GameObject canvas = GameObject.FindWithTag("PLAYER_UI");

        GameObject panel_Status = GameObject.FindGameObjectWithTag("UI_PANEL");
        Transform hpPanel = panel_Status.transform.Find("Panel_HpBar");
        Transform expPanel = panel_Status.transform.Find("Panel_ExpBar"); 

        hpBar = hpPanel.Find("HpBar").GetComponent<Image>();
        hpText = hpPanel.Find("Hp_Text").GetComponent<TextMeshProUGUI>();

        expBar = expPanel.Find("ExpBar").GetComponent<Image>();
        expText = expPanel.Find("Exp_Text").GetComponent<TextMeshProUGUI>();

        scoreText = panel_Status.transform.Find("Panel_Score").Find("ScoreText").GetComponent<TextMeshProUGUI>();
        
        playerCtrl = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Player>();
        
        hpText.text = playerCtrl.currentHp.ToString() + " / " + playerCtrl.currentExpToLevel.ToString();
        expText.text = playerCtrl.currentExp.ToString() + " / " + playerCtrl.currentExpToLevel.ToString();
        CheckScore();
        CheckPlayerHp();
        CheckPlayerExp();
        CheckPlayerAbilityGage();

        panel_Pause = canvas.transform.Find("Panel_Pause").gameObject;
        panel_Pause.SetActive(false);


        rtMaker = GameObject.FindGameObjectWithTag("RT_MAKER"); 

        consumableItemText = panel_Status.transform.Find("Panel_ConsumableItem").GetComponentInChildren<TextMeshProUGUI>();

        weaponItemText = panel_Status.transform.Find("Panel_WeaponItem").GetComponentInChildren<TextMeshProUGUI>();

        consumableItems.AddRange(rtMaker.transform.GetComponentsInChildren<ItemComponent>());

        for (int i = 0; i < consumableItems.Count; i++)
        {
            consumableItems[i].gameObject.SetActive(false);
        }
        
        weaponItems.AddRange(rtMaker_1.transform.GetComponentsInChildren<ItemComponent>());
        for (int i = 0; i < weaponItems.Count; i++)
        {
            weaponItems[i].gameObject.SetActive(false);
        }

        replayButton.onClick.AddListener(delegate { LoadMenu(); });


        CheckItem(ItemType.Consumable, playerCtrl);
        CheckItem(ItemType.Weapon, playerCtrl);

    }

    void Update()
    {

    }

    public void CheckPlayerHp()
    {
        hpText.text = playerCtrl.currentHp.ToString() + " / " + playerCtrl.currentMaxHp.ToString();

        hpBar.fillAmount = (float)playerCtrl.currentHp / (float)playerCtrl.currentMaxHp;
    }

    public void CheckPlayerExp()
    {
        expText.text = playerCtrl.currentExp.ToString() + " / " + playerCtrl.currentExpToLevel.ToString();
        expBar.fillAmount = (float)playerCtrl.currentExp / (float)playerCtrl.currentExpToLevel;

    }

    public void CheckScore()
    {
        Debug.Log("체크 스코어");
        scoreText.text = MainManager.instance.currentScore.ToString() + "/" + MainManager.instance.maxScore;
    }
    public void CheckPlayerAbilityGage()
    {
        AbilityGageText.text = playerCtrl.currentAbilityGage.ToString() + " / " + playerCtrl.maxAbilityGage.ToString();
        AbilityGageBar.fillAmount = (float)playerCtrl.currentAbilityGage / (float)playerCtrl.maxAbilityGage;
    }

    public void CheckItem(ItemType itemType, Player playerCtrl)
    {
        int count = 0;
        switch (itemType)
        {
            case ItemType.Consumable:
                ItemBase selectedConsumableItem = playerCtrl.currentConsumableItem;
                
                if (selectedConsumableItem == null)
                {
                    if (showingConsumableItem == null)
                    {
                        return;
                    }
                    showingConsumableItem.gameObject.SetActive(false);
                    consumableItemText.text = "x" + count.ToString();
                    return;
                }


                count = playerCtrl.inventory.GetItemCount(selectedConsumableItem.data.id);

                for (int i = 0; i < consumableItems.Count; i++)
                {
                    if (consumableItems[i].itemData.id != playerCtrl.currentConsumableItem.data.id)
                    {
                        continue;
                    }
                    // �ش� �����ؽ��Ŀ� �÷��̾� ������ id�� ���� ��
                    if (showingConsumableItem == null)
                    {
                        showingConsumableItem = consumableItems[i];
                        showingConsumableItem.gameObject.SetActive(true);
                        break;
                    }

                    showingConsumableItem.gameObject.SetActive(false);

                    showingConsumableItem = consumableItems[i];
                    showingConsumableItem.gameObject.SetActive(true);
                    break;
                }

                consumableItemText.text = "x" + count.ToString();

                break;
            case ItemType.Weapon:
                WeaponItemBase selectedWeaponItem = playerCtrl.currentWeapon;
                if (selectedWeaponItem == null)
                {
                    return;
                }

                count = selectedWeaponItem.weaponItemData.level;

                for (int i = 0; i < weaponItems.Count; i++)
                {
                    if (weaponItems[i].itemData.id != playerCtrl.currentWeapon.data.id)
                    {
                        continue;
                    }
                    // �ش� �����ؽ��Ŀ� �÷��̾� ������ id�� ���� ��
                    if (showingWeaponItem == null)
                    {
                        showingWeaponItem = weaponItems[i];
                        showingWeaponItem.gameObject.SetActive(true);
                        break;
                    }

                    showingWeaponItem.gameObject.SetActive(false);

                    showingWeaponItem = weaponItems[i];
                    showingWeaponItem.gameObject.SetActive(true);
                    break;
                }

                weaponItemText.text = "Lv" + count.ToString();

                break;
            default:
                break;
        }
        
    }
    public void SwitchPausePanel(bool isPaused)
    {
        panel_Pause.SetActive(isPaused);
    }

    public void DisplyEndLevelPanel()
    {
        panel_Final.SetActive(true);
        finalScore.text = MainManager.instance.currentScore.ToString();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void DisplayPlayerLevelUpTable()
    {
        if (PanelLevel.activeSelf == true)
        {
            Debug.Log("�г� ����");
            DownPlayerLevelUpTable();
            PanelLevel.SetActive(false);
            return;
        }
        PanelLevel.SetActive(true);
        TextMeshProUGUI dataText;

        PlayerLevelDataTable.constraintCount = playerCtrl.playerLevelUpData.headerOrder.Count;

        for (int i = 0; i < playerCtrl.playerLevelUpData.headerOrder.Count; i++)
        {
            // ��� ���
            dataText = Instantiate<GameObject>(cell, PlayerLevelDataTable.transform).GetComponentInChildren<TextMeshProUGUI>();
            string header = playerCtrl.playerLevelUpData.headerOrder[i + 1];
            dataText.text = header;

            Debug.Log("key:" + header);
            //playerCtrl.playerLevelUpData.headers[playerCtrl.playerLevelUpData.headerOrder[i + 1]];
        }
        for (int i = 0; i < playerCtrl.playerLevelUpData.maxLevel; i++)
        {
            Debug.Log("header: " + i);
            for (int j = 0; j < playerCtrl.playerLevelUpData.headerOrder.Count; j++)
            {
                Debug.Log("value: " + playerCtrl.playerLevelUpData.headers[playerCtrl.playerLevelUpData.headerOrder[j+1]][i]);
                dataText = Instantiate<GameObject>(dataTextPrefab.gameObject, PlayerLevelDataTable.transform).GetComponentInChildren<TextMeshProUGUI>();
                dataText.text = playerCtrl.playerLevelUpData.headers[playerCtrl.playerLevelUpData.headerOrder[j+1]][i];

            }
        }
        currentExpText.text = "Current Exp: " + playerCtrl.currentExp;
        currentLevelText.text = "Current Level: " + playerCtrl.currentLevel;
    }

    public void DownPlayerLevelUpTable()
    {
        for (int i = 0; i < PlayerLevelDataTable.transform.childCount; i++)
        {
            Destroy(PlayerLevelDataTable.transform.GetChild(i).gameObject);
        }
        
    }

}
