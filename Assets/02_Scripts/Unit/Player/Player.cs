using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Player : UnitBase, IPlayer
{
    
    GameObject currentAirCraft;

    GameObject previousAirCraft;

    [SerializeField] GameObject hydroBeamPrefab;
    [SerializeField] Transform hydroSpace;

    List<GameObject> aircrafts = new List<GameObject>();

    WeaponSpace currentWeaponSpace;
    MeshRenderer meshRenderer;

    public Inventory inventory { get { return _inventory; }}
    [SerializeField] protected Inventory _inventory;

    /// <summary> 무기 순서. Key: 무기 인덱스, value : 아이템 id </summary>
    protected List<WeaponItemBase> selectedWeapons = new List<WeaponItemBase>();
    public WeaponItemBase currentWeapon;
    public WeaponItemBase currentSpeicalWeapon;

    public int currentWeaponIdx = -1;

    /// <summary> 아이템 순서(인덱스, 아이템 키) </summary>
    public ConsumableItemBase currentConsumableItem;
    protected List<ConsumableItemBase> selectedConsumableItems = new List<ConsumableItemBase>();

    protected int currentConsumableItemIdx = -1;

    public override bool isAttacking { get { return _isAttacking; } set{ _isAttacking = value; }}
    protected bool _isAttacking;
    public Vector3 moveDir { get { return _moveDir; } set { _moveDir = value; } }
    Vector3 _moveDir;

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

    public float abilityGage { get { return _abilityGage; } set { _abilityGage = value; } }
    [SerializeField] float _abilityGage;

    public float maxAbilityGage { get { return _maxAbilityGage; } set { _maxAbilityGage = value; } }
    [SerializeField] float _maxAbilityGage;

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

        // 무기 등록 
        hydroSpace = transform.Find("HydroSpace");
        MakeBasicGun();

        currentAirCraft.transform.localPosition = Vector3.zero;

        // 메시렌더러 캐싱
        meshRenderer = currentAirCraft.GetComponent<MeshRenderer>();

        isInvincibilityBlinking = false;
        currentAirCraft.transform.position = this.transform.position;

        //하이드로빔 정보 캐싱

        base.SetFirstStatus();
        SetBody();
    }

    private void MakeBasicGun()
    {
        _inventory = gameObject.AddComponent<Inventory>();
        GunItemData gunItemData = ScriptableObject.Instantiate(Resources.Load<PistolItemData>("Datas/Weapons/PistolItemData"));

        currentWeaponSpace = currentAirCraft.GetComponentInChildren<WeaponSpace>();

        gunItemData.power = 10;
        gunItemData.level = 1;
        gunItemData.weaponSpaceTransform = currentWeaponSpace.transform;
        gunItemData.unitUser = this;
        gunItemData.attackableUser = this;
        gunItemData.teamType = teamType;

        currentWeapon = (GunItemBase)gunItemData.CreateItem();
        selectedWeapons.Add(currentWeapon);
        currentWeapon.transform.parent = currentWeaponSpace.transform;
        currentWeapon.transform.localPosition = Vector3.zero;

        GunItemData specialGun = ScriptableObject.Instantiate(Resources.Load<LaserGunItemData>("Datas/Weapons/LaserGunItemData"));
        


        specialGun.power = 0;
        specialGun.level = 1;
        specialGun.weaponSpaceTransform = hydroSpace;
        specialGun.unitUser = this;
        specialGun.attackableUser = this;
        specialGun.teamType = teamType;

        currentSpeicalWeapon = (GunItemBase)specialGun.CreateItem();
        inventory.Add(gunItemData);
        
        currentWeaponIdx++;

    }

    protected override void Update()
    {
        if (_isAttacking)
        {
            currentWeapon.Use();
        }
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

        if (isImmortal)
        {
            return;
        }

        CheckBumpedIntoEnemy(other)?.Hit(30);
    }
    protected override void OnCollisionEnter(Collision collision)
    {
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

        if (_isImmortal)
        {
            return;
        }

        isInvincibilityBlinking = true;
        SetImmortalDuring(true, 3.0f);
        StartCoroutine(InvincibilityBlink());

        Debug.Log("쳐맞음");

        UIManager.instance.CheckPlayerHp();
    }
    public override void Hit(int damage, Vector3 position)
    {
        if (isImmortal)
            return;
        currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        Debug.Log("쳐맞음");

        CheckDead();
        isInvincibilityBlinking = true;
        SetImmortalDuring(true, 3.0f);
        StartCoroutine(InvincibilityBlink());

        UIManager.instance.CheckPlayerHp();
    }
    public override void Hit(int damage, Transform hitTransform)
    {
        if (isImmortal)
            return;
        currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, hitTransform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        Debug.Log("쳐맞음");
        CheckDead();
        
        isInvincibilityBlinking = true;
        SetImmortalDuring(true, 3.0f);
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
        isAttacking = true;
    }

    public void StopAttack()
    {
        _isAttacking = false;
        currentWeapon.StopUse();
    }

    public void SpecialAttack()
    {
        currentSpeicalWeapon.Use();
    }

    public void ChangeSelectedItem<T>(int id) where T : ItemData
    {
    }

    public void UseItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Consumable:
                if (selectedConsumableItems.Count <= 0)
                {
                    currentConsumableItem = null;
                    break;
                }
                currentConsumableItem.Use();
                int count = inventory.Remove(currentConsumableItem.data.id, 1);

                if (0 == count)
                {
                    selectedConsumableItems.RemoveAt(currentConsumableItemIdx);
                    ChangeSelectedItem(itemType);
                }
                break;
            case ItemType.Weapon:
                break;
            default:
                break;
        }
        UIManager.instance.CheckItem(itemType, this);
    }

    public void GiveItem(ItemData item)
    {
        ItemType itemType = item.itemType;
        
        if (item.itemUsageType == ItemUsageType.ImmediatelyUse)
        {
            item.unitUser = this;
            item.CreateItem().Use();
            
            return;
        }

        switch (itemType)
        {
            case ItemType.Consumable:
                if (inventory.CheckExist(item.id))
                {
                    inventory.Add(item);
                    break;
                }
                currentConsumableItem = (ConsumableItemBase)inventory.Add(item).CreateItem();
                currentConsumableItem.transform.parent = transform;
                
                selectedConsumableItems.Add(currentConsumableItem);
                currentConsumableItemIdx++;

                break;
            case ItemType.Weapon:
                WeaponItemData weaponItemData;
                if (inventory.CheckExist(item.id))
                {
                    if (currentWeapon.data.id == item.id)
                    {
                        // 이미 가방안에 들어있고, 현재 선택된 무기라면
                        weaponItemData = ((GunItemData)inventory.GetItemData(item.id));
                        weaponItemData.level += 1;

                        currentWeapon = (GunItemBase)weaponItemData.CreateItem();
                        currentWeapon.weaponItemData.level = weaponItemData.level;
                        break;
                    }
                    break;
                }
                // 가방안에 없으면

                weaponItemData = (GunItemData)Instantiate<ScriptableObject>(item);

                weaponItemData.level = 1;
                weaponItemData.weaponSpaceTransform = currentWeaponSpace.transform;
                weaponItemData.unitUser = this;
                weaponItemData.attackableUser = this;
                weaponItemData.teamType = teamType;

                GameObject projectilePrefab = ((GunItemData)(currentWeapon.data)).projectilePrefab;


                currentWeapon.StopUse();
                currentWeapon = (GunItemBase)weaponItemData.CreateItem();
                currentWeapon.transform.parent = currentWeaponSpace.transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                selectedWeapons.Add(currentWeapon);
                currentWeaponIdx++;
                inventory.Add(weaponItemData);
                
                
                ChangeBullet(projectilePrefab);


                break;

            default:
                break;
        }
        UIManager.instance.CheckItem(itemType, this);

    }
    public void ChangeSelectedItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Consumable:
                if (selectedConsumableItems.Count <= 0)
                {
                    return;
                }
                currentConsumableItemIdx++;
                if (currentConsumableItemIdx >= selectedConsumableItems.Count)
                {
                    currentConsumableItemIdx = 0;
                }
                currentConsumableItem = selectedConsumableItems[currentConsumableItemIdx];

                break;
            case ItemType.Weapon:
                if (selectedWeapons.Count <= 1)
                {
                    return;
                }
                currentWeaponIdx++;

                if (currentWeaponIdx >= selectedWeapons.Count)
                {
                    currentWeaponIdx = 0;
                }
                currentWeapon.StopUse();
                currentWeapon = selectedWeapons[currentWeaponIdx];

                break;
            default:
                break;
        }
        UIManager.instance.CheckItem(itemType, this);
    }

    public void ChangeBullet(GameObject bulletPrefab)
    {
        for (int i = 0; i < selectedWeapons.Count; i++)
        {

            ((GunItemBase)selectedWeapons[i]).gunItemData.projectilePrefab = bulletPrefab;

        }
        
    }

    protected void SetBody()
    {
        Collider colliderSelf = GetComponent<Collider>();
        unitBodyColliders.AddRange(GetComponentsInChildren<UnitBody>());
        int count = unitBodyColliders.Count;

        if (colliderSelf == null)
        {
            for (int i = 0; i < count; i++)
            {
                rigidbodies.Add(unitBodyColliders[i].rigidbody);
                colliders.Add(unitBodyColliders[i].collider);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                rigidbodies.Add(unitBodyColliders[i].rigidbody);
                colliders.Add(unitBodyColliders[i].collider);
                Physics.IgnoreCollision(colliderSelf, unitBodyColliders[i].collider);
            }
        }

        for (int i = count; i > 0; i--)
        {
            for (int j = 0; j < i - 1; j++)
            {
                Physics.IgnoreCollision(colliders[i - 1], colliders[j]);
            }
        }
    }
}
