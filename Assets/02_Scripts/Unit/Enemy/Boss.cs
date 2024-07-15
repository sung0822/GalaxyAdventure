using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor.Search;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Boss : EnemyBase
{
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    bool _isAttacking;
    public int pageNumber { get { return _pageNumber; } }
    [SerializeField] int _pageNumber;

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

    public Color page2Color { get { return _page2Color; } set { _page2Color = value; } }
    [SerializeField] Color _page2Color;

    public Color page3Color { get { return _page3Color; } set { _page3Color = value; } }
    [SerializeField] Color _page3Color;

    IBossPageState _pageState;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();

        sprayGunItemData = ScriptableObject.Instantiate(sprayGunItemData);
        targetPlayer = GameObject.FindWithTag("PLAYER").transform;
               
        sprayGunItemData.power = 10;
        sprayGunItemData.weaponSpaceTransform = sprayGunWeaponSpace.transform;
        sprayGunItemData.unitUser = this;
        sprayGunItemData.attackableUser = this;
        sprayGunItemData.teamType = teamType;

        _sprayGun = (SprayGun)sprayGunItemData.CreateItem();

        _machineGunItemData.power = 10;
        _machineGunItemData.level = 1;
        _machineGunItemData.weaponSpaceTransform = machineGunWeaponSpace.transform;
        _machineGunItemData.unitUser = this;
        _machineGunItemData.attackableUser = this;
        _machineGunItemData.teamType = teamType;

        _machineGun = (MachineGun)_machineGunItemData.CreateItem();

        _pageState = new BossPageOneState();
        _pageState.boss = this;

    }

    protected override void Update()
    {
        base.Update();
        if (enableAttack)
        {
            _pageState.Attack();
        }
    }

    public override void Attack()
    {
    }

    protected override IEnumerator AdjustSpeed(float spd, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalMoveSpd = moveSpd;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // 정규화한 길이.
            float normalizedTime = timeAdjustingSpd / duration;

            if (normalizedTime >= 1)
            {
                enableAttack = true;
                break;
            }

            moveSpd = Mathf.Lerp(originalMoveSpd, spd, normalizedTime);

            yield return new WaitForEndOfFrame();
        }
    }

    public override void Hit(int damage)
    {
        if (isImmortal)
            return;

        currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, this.transform.position, this.transform.rotation);
        Destroy(particle, 0.7f);

        CheckDead();
    }

    public override void Hit(int damage, Vector3 position)
    {
        if (isImmortal)
            return;

        currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);

        CheckDead();
    }

    public override void Hit(int damage, Transform hitTransform)
    {
        if (isImmortal)
            return;

        currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, hitTransform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);

        CheckDead();
    }

    protected override bool CheckDead()
    {

        if (currentHp <= 0)
        {
            switch (pageNumber)
            {
                case 1:
                    _pageNumber++;
                    StartCoroutine(ChangeColor(page2Color, 2));
                    _pageState = new BossPageTwoState();
                    _pageState.boss = this;

                    break;
                case 2:
                    _pageNumber++;
                    StartCoroutine(ChangeColor(page3Color, 2));
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
                    Debug.Log("이상한 수가 들어옴");
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
        enableAttack = false;
        maxHp = (int)((float)maxHp * 1.5f);

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // 정규화한 길이.
            float normalizedTime = timeAdjustingSpd / duration;
            currentHp = (int)((float)maxHp * normalizedTime);
            CheckHpBarFill();

            if (normalizedTime >= 1)
            {
                SetImmortal(false);
                currentHp = maxHp;
                CheckHpBarFill();
                enableAttack = true;
                break;
            }

            skinnedMeshRenderer.material.color = Color.Lerp(originalColor, color, timeAdjustingSpd);

            yield return new WaitForEndOfFrame();

        }
    }

    void CheckHpBarFill()
    {
        hpBar.fillAmount = (float)currentHp / (float)maxHp;
    }

}
