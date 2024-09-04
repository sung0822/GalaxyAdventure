using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Boss : EnemyBase
{
    public SkinnedMeshRenderer skinnedMeshRenderer { get { return _skinnedMeshRenderer; } set { _skinnedMeshRenderer = value; } }
    [SerializeField] SkinnedMeshRenderer _skinnedMeshRenderer;

    [SerializeField] float page2Hp;
    public SprayGunItemData sprayGunItemData { get { return _sprayGunItemData; } set { _sprayGunItemData = value; } }
    [SerializeField] SprayGunItemData _sprayGunItemData;

    List<WeaponItemBase> selectedWeaponItem;
    public SprayGun sprayGun { get { return _sprayGun; } }
    [SerializeField] SprayGun _sprayGun;

    public MachineGunItemData machineGunItemData { get { return _machineGunItemData; } }
    [SerializeField] MachineGunItemData _machineGunItemData;

    public MachineGun machineGun { get { return _machineGun; } }
    [SerializeField] MachineGun _machineGun;
    public WeaponSpace sprayGunWeaponSpace { get { return _sprayGunWeaponSpace; } }
    [SerializeField] WeaponSpace _sprayGunWeaponSpace;
    public WeaponSpace machineGunWeaponSpace { get { return _machineGunWeaponSpace; } }
    [SerializeField] WeaponSpace _machineGunWeaponSpace;
    public Transform targetPlayer;
    public Image hpBar { get { return _hpBar; } set { _hpBar = value; } }
    [SerializeField] Image _hpBar;
    public TextMeshProUGUI hpText { get { return _hpText; } set { _hpText = value; } }
    [SerializeField] TextMeshProUGUI _hpText;
    IBossPageState _pageState;

    public BossData currentBossData { get { return _currentBossData; } set { _currentBossData = value; } }

    protected BossData _currentBossData;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();
        _currentBossData = (BossData)currentEnemyBaseData;

        _currentEnemyBaseData = (EnemyBaseData)_currentUnitBaseData;

        sprayGunItemData = ScriptableObject.Instantiate(sprayGunItemData);
        targetPlayer = GameObject.FindWithTag("PLAYER").transform;
               
        sprayGunItemData.power = 10;
        sprayGunItemData.weaponSpaceTransform = sprayGunWeaponSpace.transform;
        sprayGunItemData.unitUser = this;
        sprayGunItemData.attackableUser = this;
        sprayGunItemData.teamType = teamType;

        _sprayGun = (SprayGun)sprayGunItemData.CreateItem();

        _machineGunItemData = ScriptableObject.Instantiate(_machineGunItemData);

        _machineGunItemData.power = 10;
        _machineGunItemData.level = 1;
        _machineGunItemData.weaponSpaceTransform = machineGunWeaponSpace.transform;
        _machineGunItemData.unitUser = this;
        _machineGunItemData.attackableUser = this;
        _machineGunItemData.teamType = teamType;

        _machineGun = (MachineGun)_machineGunItemData.CreateItem();

        _pageState = new BossPageOneState();
        _pageState.boss = this;
        CheckHpBarFill();

    }

    protected override void Update()
    {
        base.Update();
        if (_currentEnemyBaseData.enableAttack)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        _pageState.Attack();
    }

    public override void Move()
    {
        if (_currentEnemyBaseData.enableAttack)
        {
            _pageState.Move();
            return;
        }
        base.Move();
    }

    protected override IEnumerator AdjustSpeed(float spd, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalMoveSpd = moveSpd;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // ����ȭ�� ����.
            float normalizedTime = timeAdjustingSpd / duration;

            if (normalizedTime >= 1)
            {
                _currentEnemyBaseData.enableAttack = true;
                break;
            }

            moveSpd = Mathf.Lerp(originalMoveSpd, spd, normalizedTime);

            yield return new WaitForEndOfFrame();
        }
    }

    public override void Hit(int damage)
    {
        if (_currentEnemyBaseData.isImmortal)
            return;

        _currentEnemyBaseData.currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, this.transform.position, this.transform.rotation);
        Destroy(particle, 0.7f);

        CheckDead();
    }

    public override void Hit(int damage, Vector3 position)
    {
        if (_currentEnemyBaseData.isImmortal)
            return;

        _currentEnemyBaseData.currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);

        CheckDead();
    }

    public override void Hit(int damage, Transform hitTransform)
    {
        if (_currentEnemyBaseData.isImmortal)
            return;

        _currentEnemyBaseData.currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, hitTransform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);

        CheckDead();
    }

    protected override bool CheckDead()
    {

        if (_currentEnemyBaseData.currentHp <= 0)
        {
            Debug.Log("currentPageNumber: " + currentBossData.pageNumber);
            switch (_currentBossData.pageNumber)
            {
                case 1:
                    _currentBossData.pageNumber++;
                    StartCoroutine(ChangeColor(_currentBossData.page2Color, 2));
                    _pageState = new BossPageTwoState();
                    _sprayGunItemData.useCycle -= _sprayGunItemData.useCycle * 0.5f;
                    _pageState.boss = this;

                    break;
                case 2:
                    _currentBossData.pageNumber++;
                    StartCoroutine(ChangeColor(_currentBossData.page3Color, 2));
                    _sprayGunItemData.useCycle -= _sprayGunItemData.useCycle * 0.3f;
                    _machineGunItemData.useCycle -= _machineGunItemData.useCycle * 0.5f;
                    _pageState = new BossPageThreeState();
                    _pageState.boss = this;

                    break;
                case 3:
                    if (base.CheckDead())
                    {
                        MainManager.instance.EndLevel();
                    }

                    break;
                default:
                    break;
            }
        }
        return false;
    }
    IEnumerator ChangeColor(Color color, float duration)
    {
        float timeAdjustingSpd = 0;
        SetImmortal(true);
        Color originalColor = skinnedMeshRenderer.material.color;
        _currentEnemyBaseData.enableAttack = false;
        _currentEnemyBaseData.currentMaxHp = (int)((float)_currentEnemyBaseData.currentMaxHp * 1.5f);

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // ����ȭ�� ����.
            float normalizedTime = timeAdjustingSpd / duration;
            _currentEnemyBaseData.currentHp = (int)((float)_currentEnemyBaseData.currentMaxHp * normalizedTime);
            CheckHpBarFill();

            if (normalizedTime >= 1)
            {
                SetImmortal(false);
                _currentEnemyBaseData.currentHp = _currentEnemyBaseData.currentMaxHp;
                CheckHpBarFill();
                _currentEnemyBaseData.enableAttack = true;
                break;
            }

            skinnedMeshRenderer.material.color = Color.Lerp(originalColor, color, timeAdjustingSpd);

            yield return new WaitForEndOfFrame();
        }
    }

    void CheckHpBarFill()
    {
        hpBar.fillAmount = (float)_currentEnemyBaseData.currentHp / (float)_currentEnemyBaseData.currentMaxHp;
        hpText.text = _currentEnemyBaseData.currentHp.ToString() + "/" + _currentEnemyBaseData.currentMaxHp.ToString();
    }

}
