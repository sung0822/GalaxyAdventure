using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSizeEnemy : EnemyBase
{
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


        _currentEnemyBaseData.isAttacking = false;
    }
    protected override void Update()
    {
        base.Update();

        if (currentEnemyBaseData.enableAttack)
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

        _currentEnemyBaseData.isDead = true;
        currentEnemyBaseData.enableAttack = false;
        

        GameObject particle = ParticleManager.instance.CreateParticle(currentEnemyBaseData.unitDieParticlePrefab, transform.position, transform.rotation);
        particle.transform.localScale = this.transform.localScale * 0.1f;

        Destroy(particle, 1.5f);

        ObjectPoolManager.instance.ReturnObject(currentEnemyBaseData.unitName + " Pool", this.gameObject, 10);

    }

    IEnumerator CreateExploding()
    {
        GameObject firstParticle = ParticleManager.instance.CreateParticle(currentEnemyBaseData.unitDieParticlePrefab, transform.position, transform.rotation);

        firstParticle.transform.localScale = this.transform.localScale * 2f;
        Destroy(firstParticle, 1.5f);


        while (true)
        {
            GameObject particle = ParticleManager.instance.CreateParticle(currentEnemyBaseData.unitDieParticlePrefab, transform.position, transform.rotation);

            particle.transform.localScale = this.transform.localScale * 2f;
            Destroy(particle, 1.5f);

            yield return new WaitForSeconds(1.0f);
        }
    }

    /// <summary>점점 회전시킴. 선형 보간은 아님</summary>
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
