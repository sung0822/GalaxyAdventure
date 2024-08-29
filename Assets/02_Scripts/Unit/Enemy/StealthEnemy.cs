using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StealthEnemy : EnemyBase
{
    public override bool isAttacking { get; set; }

    Vector3 moveDir;
    
    bool isInvisible = false;

    private float shootCycle = 1.0f;

    MeshRenderer meshRenderer;
    Material material;

    WeaponSpace currentWeaponSpace;
    protected WeaponItemBase currentWeapon;

    Transform targetPlayer;

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
        lifeTime = 0;

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
        isAttacking = false;

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
        enableAttack = true;
        hasCollideWithWall = true;
        StartCoroutine(AdjustSpeed(spdChanged, spdChangeDuration));
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
        moveSpd = 5.0f;
        StartCoroutine(StartAttack());

        // ���� ���̴� �Ӽ��� ����. �Ϲ������� �Ӽ� �̸����� �����ϴµ���.
        Renders.ChangeStandardShaderRenderMode(material, BlendMode.Fade);
        
        Color color = new Color(material.color.r, material.color.g, material.color.b, 1);
        material.SetColor("_Color", color);

        while (true)
        {
            // �����Ͻ� ���������� ���� �� ���� ����
            if (isInvisible)
            {
                moveDir = Vector3.zero;
                StartCoroutine(AdjustTransparency(1, 1.0f));

                isAttacking = true;
                gameObject.layer = LayerMask.NameToLayer(unitLayerName);

                for (int i = 0; i < colliders.Count; i++)
                    colliders[i].gameObject.layer = LayerMask.NameToLayer(unitLayerName);

            }// �������Ͻ� �������� ���� �� ���� ����
            else if(!isInvisible)
            {

                moveDir = Vector3.zero;
                StartCoroutine(AdjustTransparency(0, 1.0f));
                
                isAttacking = false;
                gameObject.layer = LayerMask.NameToLayer(unitLayerName);
                
                for (int i = 0; i < colliders.Count; i++)
                    colliders[i].gameObject.layer = LayerMask.NameToLayer(stealthLayerName);

            }
            isInvisible = !isInvisible;

            yield return new WaitForSeconds(3.5f);
        
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
            if (isAttacking)
            {
                Attack();
            }
            
            yield return new WaitForSeconds(shootCycle);

        }
    }

    protected IEnumerator AdjustTransparency(float transparency, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalAlpha = material.color.a;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // ����ȭ�� ����.
            float normalizedTime = timeAdjustingSpd / duration;

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

}
