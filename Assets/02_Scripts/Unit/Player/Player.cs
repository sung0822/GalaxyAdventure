using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class Player : UnitBase, IPlayer
{

    GameObject currentAirCraft;
    GameObject previousAirCraft;

    [SerializeField] GameObject hydroBeamPrefab;
    [SerializeField] Transform hydroSpace;

    public List<GameObject> aircrafts { get { return _aircrafts; } }
    [SerializeField] List<GameObject> _aircrafts = new List<GameObject>();

    public AudioSource audioSource;
    public AudioClip changeAirCraftSound;
    public AudioClip changeBulletSound;

    [SerializeField] WeaponSpace currentWeaponSpace;
    MeshRenderer meshRenderer;

    public Inventory inventory { get { return _inventory; } }
    [SerializeField] protected Inventory _inventory;

    /// <summary> 무기 순서. Key: 무기 인덱스, value : 아이템 id </summary>
    
    public List<WeaponItemBase> selectedWeapons 
    {
        get
        {
             Debug.Log("get selectedWeapons is Called"); 
             return _selectedWeapons;
        }
    }
    [SerializeField] protected List<WeaponItemBase> _selectedWeapons = new List<WeaponItemBase>();
    public WeaponItemBase currentWeapon;
    public WeaponItemBase currentSpeicalWeapon;

    public int currentWeaponIdx = -1;

    /// <summary> 아이템 순서(인덱스, 아이템 키) </summary>
    public ConsumableItemBase currentConsumableItem;
    protected List<ConsumableItemBase> selectedConsumableItems = new List<ConsumableItemBase>();

    protected int currentConsumableItemIdx = -1;
    public Vector3 moveDir { get { return _moveDir; } set { _moveDir = value; } }
    Vector3 _moveDir;
    public float currentAbilityGage { get { return _currentAbilityGage; } set { _currentAbilityGage = value; } }
    [SerializeField] float _currentAbilityGage;
    public bool isAttacking { get { return _currentPlayerData.isAttacking; } set { _currentPlayerData.isAttacking = value; } }

    public int currentLevel { get { return _currentPlayerData.currentLevel; } set { _currentPlayerData.currentLevel = value; } }

    public float currentExp { get { return _currentPlayerData.currentExp; } }

    public float moveSpd { get { return _currentPlayerData.moveSpd; } set { _currentPlayerData.moveSpd = value; } }
    public int power { get { return _currentPlayerData.power; } set { _currentPlayerData.power = value; } }
    public override TeamType teamType { get { return _currentPlayerData.teamType; } set { _currentPlayerData.teamType = value; } }

    public PlayerData currentPlayerData { get { return _currentPlayerData; } set { _currentPlayerData = value; } }

    public PlayerLevelUpData playerLevelUpData { get { return _currentPlayerData.playerLevelUpData; } set { _currentPlayerData.playerLevelUpData = value; } }

    protected PlayerData _currentPlayerData;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        Debug.Log("Player 생성됨");
        base.Start();
        CreateBasicGun();
    }
    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();
        _currentPlayerData = (PlayerData)currentUnitBaseData;

        // 비행체 등록
        currentAirCraft = _aircrafts[0];
            
        for (int i = 1; i < _aircrafts.Count; i++)
        {
            _aircrafts[i].SetActive(false);
        }

        meshRenderer = currentAirCraft.GetComponent<MeshRenderer>();
        currentAirCraft.transform.localPosition = Vector3.zero;
        
        currentAirCraft.transform.position = this.transform.position;

        // 플레이어 레벨업 데이터 등록
        currentPlayerData.playerLevelUpData = Instantiate<PlayerLevelUpData>(currentPlayerData.playerLevelUpData);
        currentPlayerData.playerLevelUpData.SetUserLevelData();

        _currentPlayerData.maxHpPerLevel.Add(_currentPlayerData.currentMaxHp);

        for (int i = 1; i < currentPlayerData.playerLevelUpData.maxLevel; i++)
        {
            _currentPlayerData.maxHpPerLevel.Add((int)(_currentPlayerData.maxHpPerLevel[i - 1] + _currentPlayerData.maxHpPerLevel[i - 1] * 0.1f));
        }
        // 무기 등록 
        hydroSpace = transform.Find("HydroSpace");
        

        

        SetBody();
    }

    private void CreateBasicGun()
    {
        _inventory = gameObject.AddComponent<Inventory>();
        GunItemData gunItemData = ScriptableObject.Instantiate(Resources.Load<PistolItemData>("Datas/Weapons/PistolItemData"));

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

        GunItemData specialGunItemData = ScriptableObject.Instantiate(Resources.Load<LaserGunItemData>("Datas/Weapons/LaserGunItemData"));


        specialGunItemData.power = 0;
        specialGunItemData.level = 1;
        specialGunItemData.weaponSpaceTransform = hydroSpace;
        specialGunItemData.unitUser = this;
        specialGunItemData.attackableUser = this;
        specialGunItemData.teamType = teamType;

        currentSpeicalWeapon = (GunItemBase)specialGunItemData.CreateItem();
        inventory.Add(gunItemData);
        
        currentWeaponIdx++;

    }

    protected override void Update()
    {
        if (_currentPlayerData.isAttacking)
        {
            currentWeapon.Use();
        }
        if (_currentPlayerData.isInvincibilityBlinking)
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
    }

    protected override void OnCollisionStay(Collision collision)
    {
    }
    public void Move()
    {
        unitRigidbody.velocity = moveDir * _currentPlayerData.moveSpd;
        
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].velocity = moveDir * _currentPlayerData.moveSpd;
        }
    }

    public void GivePlayerExp(float exp)
    {
        _currentPlayerData.currentExp += exp;

        if (_currentPlayerData.currentExp >= _currentPlayerData.currentExpToLevel)
        {
            LevelUp();
        }
        UIManager.instance.CheckPlayerExp();
    }

    public void GivePlayerAbilityGage(float abilityGage)
    {
        currentAbilityGage += abilityGage;
        if (currentAbilityGage >= _currentPlayerData.maxAbilityGage)
        {
            currentAbilityGage = _currentPlayerData.maxAbilityGage;
        }
        UIManager.instance.CheckPlayerAbilityGage();

    }

    public void LevelUp()
    {
        _currentPlayerData.currentLevel++;
        if (_currentPlayerData.currentLevel -1 >= currentPlayerData.playerLevelUpData.expToLevelUp.Count)
        {
            _currentPlayerData.currentLevel--;
            _currentPlayerData.currentExp = 0;
            _currentPlayerData.currentExpToLevel = currentPlayerData.playerLevelUpData.expToLevelUp[_currentPlayerData.currentLevel - 1];
            _currentPlayerData.currentHp = _currentPlayerData.currentMaxHp;
            UIManager.instance.CheckPlayerHp();
            UIManager.instance.CheckPlayerExp();
            return;
        }

        _currentPlayerData.currentExp = 0;

        _currentPlayerData.currentExpToLevel = currentPlayerData.playerLevelUpData.expToLevelUp[_currentPlayerData.currentLevel - 1];

        _currentPlayerData.currentMaxHp = _currentPlayerData.maxHpPerLevel[_currentPlayerData.currentLevel - 1];
        _currentPlayerData.currentHp = _currentPlayerData.currentMaxHp;

        _currentPlayerData.power += (int)(_currentPlayerData.power * 0.1f);

        ChangeAirCraft(_currentPlayerData.currentLevel -1 );

        UIManager.instance.CheckPlayerHp();
        UIManager.instance.CheckPlayerExp();
    }

    public void LevelDown()
    {
        _currentPlayerData.currentLevel--;
        if (_currentPlayerData.currentLevel <= 0)
        {
            _currentPlayerData.currentLevel++;
            return;
        }

        _currentPlayerData.currentExp = 0;

        _currentPlayerData.currentExpToLevel = currentPlayerData.playerLevelUpData.expToLevelUp[_currentPlayerData.currentLevel - 1];


        _currentPlayerData.currentMaxHp = _currentPlayerData.maxHpPerLevel[_currentPlayerData.currentLevel - 1];
        _currentPlayerData.currentHp = _currentPlayerData.currentMaxHp;

        _currentPlayerData.power -= (int)(_currentPlayerData.power * 0.1f);

        ChangeAirCraft(_currentPlayerData.currentLevel - 1);

        UIManager.instance.CheckPlayerHp();
        UIManager.instance.CheckPlayerExp();
    }

    /// <summary> 현재 레벨보다 1 낮은 수의 인덱스로 접근합니다. </summary>
    void ChangeAirCraft(int idx)
    {
        if (_currentPlayerData.currentLevel >= _aircrafts.Count + 1)
        {
            return;
        }
        // 비행체 변경
        if (currentAirCraft == null) 
        {
            Debug.Log("currentAirCraft is null");
        }

        previousAirCraft = currentAirCraft;
        previousAirCraft.transform.position = new Vector3(0, 10000, 0);
        previousAirCraft.SetActive(false);

        currentAirCraft = _aircrafts[idx];
        currentAirCraft.SetActive(true);
        Debug.Log("비행기 변경");
        
        // 변경한 비행체로 무기 어태치
        currentWeaponSpace.transform.SetParent(currentAirCraft.GetComponentInChildren<WeaponSpace>().transform);
        currentWeaponSpace.transform.localPosition = Vector3.zero;

        currentAirCraft.transform.position = this.transform.position;
        
        // 메쉬 렌더러, 필터 다시 재할당
        meshRenderer = currentAirCraft.GetComponentInChildren<MeshRenderer>();

        unitBodyColliders.Clear();
        rigidbodies.Clear();
        colliders.Clear();

        audioSource.clip = changeAirCraftSound;
        audioSource.Play();

        SetBody();

    }

    public override void Hit(int damage)
    {
        if (_currentPlayerData.isAbsoluteImmortal)
            return;

        if (_currentPlayerData.isImmortal)
            return;

        _currentPlayerData.currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, transform);
        Destroy(particle, 0.7f);

        CheckDead();
        _currentPlayerData.isInvincibilityBlinking = true;
        SetImmortalDuring(true, 3.0f);
        StartCoroutine(InvincibilityBlink());
        UIManager.instance.CheckPlayerHp();
    }
    public override void Hit(int damage, Vector3 position)
    {
        if (_currentPlayerData.isAbsoluteImmortal)
            return;

        if (_currentPlayerData.isImmortal)
            return;

        _currentPlayerData.currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);

        CheckDead();
        _currentPlayerData.isInvincibilityBlinking = true;
        SetImmortalDuring(true, 3.0f);
        StartCoroutine(InvincibilityBlink());
        UIManager.instance.CheckPlayerHp();
    }
    public override void Hit(int damage, Transform hitTransform)
    {
        if (_currentPlayerData.isAbsoluteImmortal)
            return;

        if (_currentPlayerData.isImmortal)
            return;

        _currentPlayerData.currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, hitTransform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        Debug.Log("쳐맞음");
        CheckDead();

        _currentPlayerData.isInvincibilityBlinking = true;
        SetImmortalDuring(true, 3.0f);
        StartCoroutine(InvincibilityBlink());

        UIManager.instance.CheckPlayerHp();
    }

    public override void DieUnit()
    {
        _currentPlayerData.isDead = true;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.unitExplodingParticle, transform.position, transform.rotation);

        particle.transform.localScale = this.transform.localScale * 0.1f;
        Destroy(particle, 1.5f);

        MainManager.instance.EndLevel();
    }

    IEnumerator InvincibilityBlink()
    {
        Renders.ChangeStandardShaderRenderMode(meshRenderer.material, BlendMode.Transparent);

        while (_currentPlayerData.isInvincibilityBlinking) 
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

                if (!_currentPlayerData.isImmortal)
                {
                    _currentPlayerData.isInvincibilityBlinking = false;
                }
            }

            yield return new WaitForSeconds(0.15f);
        }

        Renders.ChangeStandardShaderRenderMode(meshRenderer.material, BlendMode.Opaque);

    }

    public void Attack()
    {
        _currentPlayerData.isAttacking = true;
    }

    public void StopAttack()
    {
        _currentPlayerData.isAttacking = false;
        currentWeapon.StopUse();
    }

    public void SpecialAttack()
    {
        Debug.Log("currentAbilityGage: " + currentAbilityGage);
        if (currentAbilityGage < _currentPlayerData.maxAbilityGage)
        {
            Debug.Log("들어옴");
            return;
        }
        currentAbilityGage = 0;
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

                if (currentConsumableItem == null)
                {
                    return;
                }

                currentConsumableItem.Use();
                int count = inventory.Remove(currentConsumableItem.data.id, 1);
                Debug.Log(count);
                
                if (0 == count)
                {
                    Debug.Log("currentConsumableItemIdx: " + currentConsumableItemIdx);
                    Destroy(selectedConsumableItems[currentConsumableItemIdx].transform.gameObject);
                    selectedConsumableItems.RemoveAt(currentConsumableItemIdx);
                    currentConsumableItemIdx--;
                    ChangeSelectedItem(itemType);
                    
                }
                if (selectedConsumableItems.Count <= 0)
                {
                    currentConsumableItem = null;
                    break;
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
            ItemBase currentItem = item.CreateItem();
            currentItem.Use();
            Destroy(currentItem.transform.gameObject);
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
                        Debug.Log("가방안에 들어있고, 선택된 무기임");
                        currentWeapon.weaponItemData.level += 1;
                        break;
                    }

                    Debug.Log("가방안에 들어있고, 선택되지 않은 무기임");
                    for (int i = 0; i < selectedWeapons.Count; i++)
                    {
                        if (selectedWeapons[i].data.id == item.id)
                        {
                            selectedWeapons[i].weaponItemData.level += 1;
                        }
                        audioSource.clip = changeBulletSound;
                        audioSource.Play();
                    }
                    break;
                }
                else
                {
                    // 가방안에 없으면
                    Debug.Log("가방안에 없음");
                    weaponItemData = (GunItemData)Instantiate<ScriptableObject>(item);
                    weaponItemData.level = 1;
                    weaponItemData.weaponSpaceTransform = currentWeaponSpace.transform;
                    weaponItemData.unitUser = this;
                    weaponItemData.attackableUser = this;
                    weaponItemData.teamType = teamType;
                    
                    currentWeapon.StopUse();
                    currentWeapon = (GunItemBase)weaponItemData.CreateItem();
                    currentWeapon.transform.parent = currentWeaponSpace.transform;
                    currentWeapon.transform.localPosition = Vector3.zero;
                    
                    selectedWeapons.Add(currentWeapon);
                    currentWeaponIdx++;
                    inventory.Add(weaponItemData);
                    GameObject projectilePrefab = ((GunItemData)(currentWeapon.data)).projectilePrefab;    
                    ChangeBullet(projectilePrefab);
                    audioSource.clip = changeBulletSound;
                    audioSource.Play();
                }

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
                audioSource.clip = changeBulletSound;
                audioSource.Play();

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

    public override void SetImmortalDuring(bool isImmortal, float time)
    {
        base.SetImmortalDuring(isImmortal, time);
    }
}
