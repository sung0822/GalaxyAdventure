using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSizeEnemy : EnemyBase
{
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    private bool _isAttacking = false;

    [SerializeField] GunItemData gunItemData;
    WeaponSpace currentWeaponSpace;
    WeaponItemBase currentWeapon = null;
    Transform targetPlayer;

    protected override void Start()
    {
        base.Start();
    }
    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();

        currentWeaponSpace = transform.GetComponentInChildren<WeaponSpace>();

        gunItemData = ScriptableObject.Instantiate(gunItemData);
        
        gunItemData.power = 10;
        gunItemData.level = 1;
        gunItemData.weaponSpaceTransform = currentWeaponSpace.transform;
        gunItemData.unitUser = this;
        gunItemData.attackableUser = this;
        gunItemData.teamType = teamType;

        targetPlayer = GameObject.FindWithTag("PLAYER").transform;
        currentWeapon = (GunItemBase)gunItemData.CreateItem();


        isAttacking = false;
    }
    protected override void Update()
    {
        base.Update();

        if (enableAttack)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        currentWeaponSpace.transform.LookAt(targetPlayer);
        currentWeapon.Use();
    }

    public override void DieUnit()
    {
        DieEnemy();

        StartCoroutine(CreateExploding());

        unitRigidbody.useGravity = true;
        unitRigidbody.isKinematic = false;
        unitRigidbody.constraints = RigidbodyConstraints.None;
        unitRigidbody.detectCollisions = false;

        unitRigidbody.AddForce(Vector3.forward * 50);

        StartCoroutine(RotateGradually(new Vector3(0, 0, 20), 10));

        for (int i = 0; i < rigidbodies.Count; i++)
        {   
            rigidbodies[i].useGravity = true;
            rigidbodies[i].isKinematic = false;
            rigidbodies[i].constraints = RigidbodyConstraints.None;
            rigidbodies[i].detectCollisions = false;
        }

        Destroy(this.gameObject, 10.0f);
        isDead = true;
        enableAttack = false;
    }

    IEnumerator CreateExploding()
    {
        GameObject firstParticle = ParticleManager.instance.CreateParticle(ParticleManager.instance.unitExplodingParticle, transform.position, transform.rotation);

        firstParticle.transform.localScale = this.transform.localScale * 2f;
        Destroy(firstParticle, 1.5f);


        while (true)
        {
            GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.unitExplodingParticle, transform.position, transform.rotation);

            particle.transform.localScale = this.transform.localScale * 2f;
            Destroy(particle, 1.5f);

            yield return new WaitForSeconds(1.0f);
        }
    }

    /// <summary>���� ȸ����Ŵ. ���� ������ �ƴ�</summary>
    IEnumerator RotateGradually(Vector3 eulerPerSec, float duration)
    {
        float time = 0;
        while (time <= duration)
        {
            transform.Rotate(eulerPerSec * Time.deltaTime, Space.Self);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
