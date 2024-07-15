using UnityEngine;
using UnityEngine.EventSystems;
public interface IBossPageState
{
    Boss boss { get; set; }
    void Attack();

    void Move();

}

public abstract class BossPageState : IBossPageState
{
    protected delegate void Attacks();
    protected Attacks[] attacks;
    public Boss boss { get; set; }
    protected Attacks currentAttack;
    protected float timeElapsed = 0;
    protected float attackPatternInterval = 5.0f;
    protected int currentAttackIdx = 1;

    float moveDistanceTotal = 0;
    bool ismovingRight;
    Vector3 moveDir;

    public abstract void Attack();
    protected virtual void AttackPatternOne()
    {
        boss.sprayGun.Use();
        boss.sprayGunItemData.weaponSpaceTransform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
        Debug.Log("패턴1: 호출");

    }
    protected virtual void AttackPatternTwo()
    {
        boss.machineGun.Use();
        boss.machineGunWeaponSpace.transform.LookAt(boss.targetPlayer);
        Debug.Log("패턴2: 호출");
    }

    protected virtual void AttackPatternThree()
    {
        boss.sprayGun.Use();
        boss.sprayGunItemData.weaponSpaceTransform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);

        boss.machineGun.Use();
        boss.machineGunWeaponSpace.transform.LookAt(boss.targetPlayer);
        Debug.Log("패턴1: 호출");
    }

    public virtual void Move()
    {
        if (moveDistanceTotal >= 3)
        {
            ismovingRight = false;
        }
        else if (moveDistanceTotal <= -3)
        {
            ismovingRight = true;
        }


        if (ismovingRight)
        {
            moveDir = Vector3.right * Time.deltaTime * 2;
            Debug.Log("움직인 거리: " + moveDistanceTotal);
            moveDistanceTotal += moveDir.magnitude;
        }
        else
        {
            moveDistanceTotal -= moveDir.magnitude;
            moveDir = Vector3.right * Time.deltaTime * -2;
            Debug.Log("움직인 거리: " + moveDistanceTotal);
        }

        boss.transform.Translate(moveDir, Space.Self);
    }
}
public class BossPageOneState : BossPageState
{

    public BossPageOneState()
    {
        attacks = new Attacks[] { AttackPatternOne, AttackPatternTwo };
        currentAttack = attacks[currentAttackIdx]; // 초기 공격 패턴 설정
    }

    public override void Attack()
    {
        timeElapsed += Time.deltaTime;
        currentAttack();
        Debug.Log("호출됨");
        if (timeElapsed >= attackPatternInterval)
        {
            currentAttackIdx++;
            if (currentAttackIdx >= attacks.Length)
            {
                boss.machineGun.StopUse();
                boss.sprayGun.StopUse();
                currentAttackIdx = 0;
            }

            currentAttack = attacks[currentAttackIdx];
            timeElapsed = 0;
            return;
        }
    }

    public override void Move()
    {
    }

}
public class BossPageTwoState : BossPageState
{
    public BossPageTwoState()
    {
        attacks = new Attacks[] { AttackPatternOne, AttackPatternTwo, AttackPatternThree };
        currentAttack = attacks[currentAttackIdx]; // 초기 공격 패턴 설정
    }
    public override void Attack()
    {
        timeElapsed += Time.deltaTime;
        currentAttack();
        if (timeElapsed >= attackPatternInterval)
        {
            currentAttackIdx = Random.Range(0, attacks.Length);
            currentAttack = attacks[currentAttackIdx];
            timeElapsed = 0;
            return;
        }
    }

    protected override void AttackPatternOne()
    {
        boss.sprayGun.Use();
        boss.sprayGunItemData.weaponSpaceTransform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
        Move();
    }

    public override void Move()
    {
        switch (currentAttackIdx)
        {
            case 0:
                base.Move();

                break;

            default:
                break;
        }
    }

}
public class BossPageThreeState : BossPageState
{
    public BossPageThreeState()
    {
        attacks = new Attacks[] { AttackPatternOne, AttackPatternTwo, AttackPatternThree };
        currentAttack = attacks[currentAttackIdx]; // 초기 공격 패턴 설정
    }
    public override void Attack()
    {
        timeElapsed += Time.deltaTime;
        currentAttack();
        if (timeElapsed >= attackPatternInterval)
        {
            currentAttackIdx = Random.Range(0, attacks.Length);

            currentAttack = attacks[currentAttackIdx];
            timeElapsed = 0;
            return;
        }
    }

    protected override void AttackPatternOne()
    {
        boss.sprayGun.Use();
        boss.sprayGunItemData.weaponSpaceTransform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
        Move();
    }
    protected override void AttackPatternTwo()
    {
        boss.machineGun.Use();
        boss.machineGunWeaponSpace.transform.LookAt(boss.targetPlayer);
        Move();
    }
    public override void Move()
    {
        switch (currentAttackIdx)
        {
            case 0:
                base.Move();

                break;
            case 1:
                base.Move();
                
                break;
            default:
                break;
        }
    }
}
