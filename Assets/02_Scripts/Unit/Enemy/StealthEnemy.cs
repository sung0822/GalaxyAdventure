using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StealthEnemy : EnemyBase
{
    public override bool isAttacking { get; set; }
    protected override float spdChanged { get { return _spdChanged; } set { _spdChanged = value; } }
    private float _spdChanged = 1.0f;
    protected override float spdChangeDuration { get { return _spdChangeDuration; } set { _spdChangeDuration = value; } }
    private float _spdChangeDuration = 0.75f;

    public override int power { get { return _power; } set { _power = value; } }
    [SerializeField] int _power = 10;
    public override int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField] int _maxHp = 100;
    public override int currentHp { get { return _currentHp; } set { _currentHp = value; } }
    [SerializeField] int _currentHp = 100;
    public override float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField] float _moveSpd = 10;

    protected override int rewardExp { get { return _rewardExp; } set { _rewardExp = value; } }
    [SerializeField] int _rewardExp = 10;
    protected override int rewardScore { get { return _rewardScore; } set { _rewardScore = value; } }
    [SerializeField] protected int _rewardScore = 100;

    Vector3 moveDir;
    
    bool isInvisible = false;

    private float shootCycle = 1.0f;

    MeshRenderer meshRenderer;
    Material material;

    Transform middleTransform;

    Transform targetPlayer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();
        lifeTime = 0;
        
        middleTransform = transform.GetComponentInChildren<WeaponSpace>().transform;
        BasicGun basicGun =  middleTransform.AddComponent<BasicGun>();
        weapons.Add(basicGun);

        currentWeapon = weapons[0];
        currentWeapon.user = this;
        currentWeapon.teamType = this.teamType;
        isAttacking = false;

        targetPlayer = GameObject.FindWithTag("PLAYER").transform;
        moveDir = Vector3.forward;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
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
        material.SetFloat("_Mode", 2.0f);
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

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
                Debug.Log("���� ����");

                for (int i = 0; i < rigidbodies.Count; i++)
                {
                    rigidbodies[i].detectCollisions = true;
                }
                unitRigidbody.detectCollisions = true;

            }// �������Ͻ� �������� ���� �� ���� ����
            else if(!isInvisible)
            {
                //do
                //{
                //    horizontalDirection = Random.Range(-1, 2);
                //}
                //while (horizontalDirection == 0);

                //moveDir = Vector3.right * horizontalDirection;
                moveDir = Vector3.zero;
                StartCoroutine(AdjustTransparency(0, 1.0f));
                
                isAttacking = false;
                Debug.Log("���� ����");

                for (int i = 0; i < rigidbodies.Count; i++)
                {
                    rigidbodies[i].detectCollisions = false;
                }
                unitRigidbody.detectCollisions = false;
            }
            isInvisible = !isInvisible;

            yield return new WaitForSeconds(3.5f);
        
        }

    }

    
    public override void Attack()
    {
        currentWeapon.transform.LookAt(targetPlayer);
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
