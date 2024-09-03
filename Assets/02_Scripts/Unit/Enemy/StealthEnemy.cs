using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StealthEnemy : EnemyBase
{

    Vector3 moveDir;
    
    bool isInvisible = false;

    private float shootCycle = 1.0f;

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material material;

    [SerializeField] WeaponSpace currentWeaponSpace;
    [SerializeField] protected WeaponItemBase currentWeapon;

    [SerializeField] Transform targetPlayer;

    [SerializeField] GunItemData gunItemData;

    [SerializeField] string stealthLayerName;
    [SerializeField] string unitLayerName;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();
        currentEnemyBaseData.lifeTime = 0;

        gunItemData = ScriptableObject.Instantiate(gunItemData);

        currentWeaponSpace = GetComponentInChildren<WeaponSpace>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        
        gunItemData.power = 10;
        gunItemData.level = 1;
        gunItemData.weaponSpaceTransform = currentWeaponSpace.transform;
        gunItemData.unitUser = this;
        gunItemData.attackableUser = this;
        gunItemData.teamType = teamType;

        gunItemData.useCycle = 1.5f;
        gunItemData.forceForProjectile = 8;
        gunItemData.level = 1;
        
        currentWeapon = (GunItemBase)gunItemData.CreateItem();
        _currentEnemyBaseData.isAttacking = false;

        targetPlayer = GameObject.FindWithTag("PLAYER").transform;
        moveDir = Vector3.forward;
        material = meshRenderer.material;
        
    }

    protected override void Update()
    {
        base.Update();
    }


    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void HandleColliderWall()
    {
        currentEnemyBaseData.enableAttack = true;
        currentEnemyBaseData.hasCollideWithWall = true;
        StartCoroutine(AdjustSpeed(currentEnemyBaseData.spdChanged, currentEnemyBaseData.spdChangeDuration));
        SetImmortal(false);
        StartCoroutine(ChangeVisibility());
    }

    public override void Move()
    {
        transform.Translate(moveDir * moveSpd * Time.deltaTime, Space.Self);
    }

    IEnumerator ChangeVisibility()
    {
        yield return new WaitForSeconds(1.0f);
        moveSpd = 0.0f;
        StartCoroutine(StartAttack());
        
        Color color = new Color(material.color.r, material.color.g, material.color.b, 1);
        material.SetColor("_Color", color);

        while (true)
        {
            if (isInvisible) // Make it visible if it is invisible
            {
                yield return StartCoroutine(AdjustTransparency(1, 1.0f));
                _currentEnemyBaseData.isAttacking = true;
            }
            else if(!isInvisible) // Make it invisible if it is visible
            {
                yield return StartCoroutine(AdjustTransparency(0, 1.0f));
                _currentEnemyBaseData.isAttacking = false;
            }
            isInvisible = !isInvisible;

            yield return new WaitForSeconds(3.5f);
        
        }

    }

    protected IEnumerator AdjustTransparency(float transparency, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalAlpha = material.color.a;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            float normalizedTime = timeAdjustingSpd / duration;

            Debug.Log("normalized time is: " + normalizedTime);
            if (normalizedTime >= 1)
            {
                break;
            }

            Color color = material.color;
            float alpha = color.a;
            alpha = Mathf.Lerp(originalAlpha, transparency, normalizedTime);
            Color newColor = new Color(color.r, color.g, color.b, alpha);

            material.SetColor("_Color", newColor);

            yield return new WaitForEndOfFrame();
        }
    }

    public override void Attack()
    {
        currentWeaponSpace.transform.LookAt(targetPlayer);
        currentWeapon.Use();
    }

    IEnumerator StartAttack()
    {
        while (true)
        {
            if (_currentEnemyBaseData.isAttacking)
            {
                Attack();
            }
            
            yield return new WaitForSeconds(shootCycle);

        }
    }
}
