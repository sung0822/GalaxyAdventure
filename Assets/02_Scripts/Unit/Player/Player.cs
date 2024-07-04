using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : Unit, IPlayer
{
    public Rigidbody rigidbody;

    GameObject currentAirCraft;

    GameObject previousAirCraft;

    WeaponBase currentWeapon;
    List<GameObject> aircrafts = new List<GameObject>();

    WeaponSpace weaponSpace;

    public GameObject basicGunPrefab;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    Inventory ICarryable.inventory { get; set; }

    public Inventory inventory { get { return _inventory; }}
    protected Inventory _inventory = new Inventory();

    public List<WeaponBase> weapons = new List<WeaponBase>();

    public ItemBase selectedItem;
    
    public int currentWeaponIdx = 0;

    /// <summary>
    /// 적재된 순서대로 아이템 순서가 생김. currentItemIdx로 접근 
    /// </summary>
    Dictionary<int, int> itemOder = new Dictionary<int, int>();

    protected int currentItemIdx = -1;

    public override bool isAttacking { get { return _isAttacking; } set{ _isAttacking = value; }}
    protected bool _isAttacking;
     
    public Vector3 moveDir { get { return _moveDir; } set { _moveDir = value; } }
    Vector3 _moveDir;
    public int currentLevel { get { return _currentLevel; } set { _currentLevel = value; } }
    public float currentExp { get { return _currentExp; } set { _currentExp = value; } }
    public float maxExp { get { return _maxExp; } set { _maxExp = value; } }

    public int maxLevel { get { return _maxLevel; } set { _maxLevel = value; } }

    public float abilityGage { get; set; }

    public int _maxLevel = 10;

    protected float previousMaxExp;
    protected int previousMaxHp;

    [SerializeField] int _currentLevel = 1;
    [SerializeField] float _currentExp = 0;
    [SerializeField] float _maxExp = 100;

    public bool isInvincibilityBlinking;

    public float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    protected float _moveSpd = 10;

    protected override void Start()
    {
        base.Start();
        _myTeam = TeamType.ALLY;

        rigidbody = GetComponent<Rigidbody>();

        // 비행기 메쉬 캐싱 및 현재 비행기 활성화
        {
            aircrafts.Add(transform.Find("Player_0").gameObject);
            aircrafts.Add(transform.Find("Player_1").gameObject);
            aircrafts.Add(transform.Find("Player_2").gameObject);
            aircrafts.Add(transform.Find("Player_3").gameObject);
            aircrafts.Add(transform.Find("Player_4").gameObject);
        }

        currentAirCraft = aircrafts[0];

        for (int i = 1; i < aircrafts.Count; i++)
        {
            aircrafts[i].SetActive(false);
        }

        // 무기 등록 
        weaponSpace = currentAirCraft.GetComponentInChildren<WeaponSpace>();

        GameObject gameObject = GameObject.Instantiate<GameObject>(basicGunPrefab, weaponSpace.transform);
        gameObject.name = basicGunPrefab.name;

        weapons.Add(gameObject.GetComponent<BasicGun>());
        weapons[currentWeaponIdx].shootCycle = 0.2f;
        weapons[currentWeaponIdx].user = this;

        currentWeapon = weapons[currentWeaponIdx];
        currentAirCraft.transform.position = transform.position;


        // 메시렌더러 캐싱
        
        meshRenderer = currentAirCraft.GetComponent<MeshRenderer>();
        meshFilter = currentAirCraft.GetComponent<MeshFilter>();

        isInvincibilityBlinking = false;

    }

    protected override void Update()
    {
        base.Update();
        
        Attack();

        if (isInvincibilityBlinking)
        {
            return;
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player Collision충돌: " + collision.gameObject.name);
    }

    protected override void OnCollisionStay(Collision collision)
    {
    }


    public void Move()
    {
        rigidbody.velocity = moveDir * moveSpd;
    }

    public void GivePlayerExp(float exp)
    {
        currentExp += exp;
        UIManager.instance.CheckPlayerExp();

        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentLevel++;

        currentExp = 0;

        previousMaxExp = maxExp;
        maxExp = maxExp * 1.5f;

        previousMaxHp = maxHp;

        maxHp = maxHp + (int)(maxHp * 0.1f);
        currentHp = maxHp;

        power += (int)(power * 0.1f);

        ChangeAirCraft(currentLevel -1 );

        UIManager.instance.CheckPlayerHp();
        UIManager.instance.CheckPlayerExp();
    }

    public void LevelDown()
    {
        currentLevel--;

        maxExp = previousMaxExp;
        maxHp = previousMaxHp;

        power += (int)(power * 0.1f);

        ChangeAirCraft(currentLevel - 1);

        UIManager.instance.CheckPlayerHp();
        UIManager.instance.CheckPlayerExp();
    }

    void ChangeAirCraft(int idx)
    {
        if (currentLevel >= aircrafts.Count + 1)
        {
            return;
        }
        // 비행체 변경
        previousAirCraft = currentAirCraft;
        previousAirCraft.transform.position = new Vector3(0, 10000, 0);
        previousAirCraft.SetActive(false);

        currentAirCraft = aircrafts[idx];
        currentAirCraft.SetActive(true);
        
        // 변경한 비행체로 무기 어태치
        currentWeapon.transform.SetParent(currentAirCraft.GetComponentInChildren<WeaponSpace>().transform);
        currentWeapon.transform.localPosition = Vector3.zero;

        currentAirCraft.transform.position = this.transform.position;
        
        // 메쉬 렌더러, 필터 다시 재할당
        meshRenderer = currentAirCraft.GetComponent<MeshRenderer>();
        meshFilter = currentAirCraft.GetComponent<MeshFilter>();
        
    }

    public override void Hit(int damage)
    {
        if (isImmortal)
        {
            return;
        }
        currentHp -= damage;
        CheckDead();

        _isImmortal = true;
        isInvincibilityBlinking = true;

        StartCoroutine(SetImmortal(false, 2.0f));
        StartCoroutine(InvincibilityBlink());

        UIManager.instance.CheckPlayerHp();
    }

    public override IEnumerator SetImmortal(bool isImmortal, float immortalTime)
    {
        yield return base.SetImmortal(isImmortal, immortalTime);
    }

    IEnumerator InvincibilityBlink()
    {
        meshRenderer.material.SetFloat("_Mode", 3);

        while (isInvincibilityBlinking) 
        {
                // 반투명으로 변경
            if (meshRenderer.material.color.a == 1.0f)
            {
                Color color = meshRenderer.material.color;
                color.a = 0.7f;
                
                meshRenderer.material.color = color;
                
            }   // 불투명으로 변경
            else
            {
                Color color = meshRenderer.material.color;
                color.a = 1.0f;
                
                meshRenderer.material.color = color;

                if (!isImmortal)
                {
                    isInvincibilityBlinking = false;
                }
            }

            yield return new WaitForSeconds(0.15f);
        }

        meshRenderer.material.SetFloat("_Mode", 0);

    }

    protected void Attack()
    {
        if(_isAttacking)
        {
            currentWeapon.Use();
        }
    }

    public void SpecialAttack()
    {
        Instantiate<GameObject>(Resources.Load<GameObject>("Items/Bomber"));
    }

    public void ChangeSelectedItem()
    {
        currentItemIdx += 1;
        Debug.Log("currentItemIdx: " + currentItemIdx);
        if (currentItemIdx == itemOder.Count)
        {
            Debug.Log("처음 인덱스로");
            currentItemIdx = 0;
        }
        selectedItem = inventory.GetItem(itemOder[currentItemIdx]);
        
        UIManager.instance.CheckItem();
    }

    public void UseItem()
    {
        selectedItem.Use();
    }

    public void GiveItem(ItemBase item)
    {
        if(inventory.CheckExist(item))
        {
            inventory.Add(item);
        }
        else
        {
            inventory.Add(item);
            currentItemIdx++;

            itemOder.Add(itemOder.Count, item.id);
            selectedItem = item;
        }

        UIManager.instance.CheckItem();
    }


}
