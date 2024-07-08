using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : UnitBase, IPlayer
{

    GameObject currentAirCraft;

    GameObject previousAirCraft;

    List<GameObject> aircrafts = new List<GameObject>();

    WeaponSpace currentWeaponSpace;
    public GameObject basicGunPrefab;
    MeshRenderer meshRenderer;

    public Inventory inventory { get { return _inventory; }}
    protected Inventory _inventory = new Inventory();

    public List<WeaponBase> weapons = new List<WeaponBase>();
    WeaponBase selectedWeapon;
    Dictionary<int, int> weaponOrder = new Dictionary<int, int>();
    public int currentWeaponIdx = 0;

    Dictionary<int, int> consumableItemOder = new Dictionary<int, int>();
    protected int currentConsumableItemIdx = -1;
    public ItemBase selectedConsumableItem;
    

    /// <summary>
    /// 적재된 순서대로 아이템 순서가 생김. currentItemIdx로 접근 
    /// </summary>

    public override bool isAttacking { get { return _isAttacking; } set{ _isAttacking = value; }}
    protected bool _isAttacking;
    public Vector3 moveDir { get { return _moveDir; } set { _moveDir = value; } }
    Vector3 _moveDir;

    public override int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField] int _maxHp = 100;
    public override int currentHp { get { return _currentHp; } set { _currentHp = value; } }
    [SerializeField] int _currentHp = 100;
    public int currentLevel { get { return _currentLevel; } set { _currentLevel = value; } }
    [SerializeField] int _currentLevel = 1;
    public float currentExp { get { return _currentExp; } set { _currentExp = value; } }
    [SerializeField] float _currentExp = 0;
    public float maxExp { get { return _maxExp; } set { _maxExp = value; } }
    [SerializeField] float _maxExp = 100;
    public int maxLevel { get { return _maxLevel; } set { _maxLevel = value; } }
    [SerializeField] int _maxLevel = 10;
    public int power { get { return _power; } set { _power = value; } }
    [SerializeField] public int _power = 10;

    public float abilityGage { get; set; }

    protected float previousMaxExp;
    protected int previousMaxHp;

    public bool isInvincibilityBlinking;

    public float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }

    public override TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    private TeamType _teamType = TeamType.ALLY;

    protected float _moveSpd = 10;

    protected override void Start()
    {
        base.Start();
    }
    protected override void SetFirstStatus()
    {
        ChangeAirCraft(0);
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

        base.SetFirstStatus();
            
        // 무기 등록 
        currentWeaponSpace = currentAirCraft.GetComponentInChildren<WeaponSpace>();

        weapons.Add(new BasicGun(this, this, currentWeaponSpace));
        weapons[currentWeaponIdx].useCycle = 0.2f;

        selectedWeapon = weapons[currentWeaponIdx];
        
        selectedWeapon.weaponSpace = currentWeaponSpace;
        selectedWeapon.attackableUser = this;
        selectedWeapon.Use();

        currentAirCraft.transform.localPosition = Vector3.zero;
        Debug.Log("플레이어 생성됨");

        // 메시렌더러 캐싱
        meshRenderer = currentAirCraft.GetComponent<MeshRenderer>();

        isInvincibilityBlinking = false;
        currentAirCraft.transform.position = this.transform.position;
    }
    protected override void Update()
    {
        currentAirCraft.transform.localPosition = Vector3.zero;
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
        Debug.Log(name + ":Trigger 발동");

        if (isImmortal)
        {
            return;
        }

        CheckBumpedIntoEnemy(other)?.Hit(30);
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
        unitRigidbody.velocity = moveDir * moveSpd;
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].velocity = moveDir * moveSpd;
        }
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

    /// <summary> 현재 레벨보다 1 낮은 수의 인덱스로 접근합니다. </summary>
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
        currentWeaponSpace.transform.SetParent(currentAirCraft.GetComponentInChildren<WeaponSpace>().transform);
        currentWeaponSpace.transform.localPosition = Vector3.zero;

        currentAirCraft.transform.position = this.transform.position;
        
        // 메쉬 렌더러, 필터 다시 재할당
        meshRenderer = currentAirCraft.GetComponent<MeshRenderer>();

        unitBodyColliders.Clear();
        rigidbodies.Clear();
        colliders.Clear();

        SetBody();

    }

    public override void Hit(int damage)
    {
        base.Hit(damage);

        _isImmortal = true;
        isInvincibilityBlinking = true;

        StartCoroutine(SetImmortal(false, 2.0f));
        StartCoroutine(InvincibilityBlink());

        UIManager.instance.CheckPlayerHp();
    }
    public override void Hit(int damage, Vector3 position)
    {
        base.Hit(damage, position);

        _isImmortal = true;
        isInvincibilityBlinking = true;

        StartCoroutine(SetImmortal(false, 2.0f));
        StartCoroutine(InvincibilityBlink());

        UIManager.instance.CheckPlayerHp();
    }
    public override void Hit(int damage, Transform transform)
    {
        base.Hit(damage, transform);

        _isImmortal = true;
        isInvincibilityBlinking = true;

        StartCoroutine(SetImmortal(false, 2.0f));
        StartCoroutine(InvincibilityBlink());

        UIManager.instance.CheckPlayerHp();
    }

    IEnumerator InvincibilityBlink()
    {
        Renders.ChangeStandardShader(meshRenderer.material, BlendMode.Transparent);

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

        Renders.ChangeStandardShader(meshRenderer.material, BlendMode.Opaque);

    }

    public void Attack()
    {
        if(_isAttacking)
            selectedWeapon.Use();
    }

    public void SpecialAttack()
    {
        Instantiate<GameObject>(Resources.Load<GameObject>("Items/Bomber"));
    }

    public void ChangeSelectedItem()
    {
        currentConsumableItemIdx += 1;
        Debug.Log("currentItemIdx: " + currentConsumableItemIdx);
        if (currentConsumableItemIdx == consumableItemOder.Count)
        {
            Debug.Log("처음 인덱스로");
            currentConsumableItemIdx = 0;
        }
        selectedConsumableItem = inventory.GetItem(consumableItemOder[currentConsumableItemIdx], ItemType.Consumable);
        
        UIManager.instance.CheckItem();
    }

    public void UseItem()
    {
        selectedConsumableItem.Use();
    }

    public void GiveItem(ItemBase item)
    {
        if(inventory.CheckExist(item.id, item.itemType))
        {
            inventory.Add(item);
        }
        else
        {
            inventory.Add(item);

            switch (item.itemType)
            {
                case ItemType.Consumable:
                    consumableItemOder.Add(consumableItemOder.Count, item.id);
                    currentConsumableItemIdx++;

                    break;
                case ItemType.Weapon:
                    
                    weaponOrder.Add(weaponOrder.Count, item.id);
                    currentWeaponIdx++;
                    break;

                default:
                    break;
            }

            selectedConsumableItem = item;
        }

        UIManager.instance.CheckItem();
    }

    
}
