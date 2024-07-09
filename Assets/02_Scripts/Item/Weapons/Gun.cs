using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : WeaponBase, IFireable
{
    public abstract List<IShootable> loadedBullets { get; }
    public abstract AudioClip audioClip { get; set; }
    public override UnitBase user { get; set; }

    private bool isShootable = true;
    public override WeaponSpace weaponSpace { get; set; }
    public override ItemUsageType usageType { get { return _usageType; } }
    private ItemUsageType _usageType = ItemUsageType.ImmediatelyUse;

    private float shootCycleValue = 1.5f;


    public Gun(UnitBase user, IAttackable attackableUser, WeaponSpace weaponSpace) : base(user, attackableUser, weaponSpace)
    {
        audioClip = Resources.Load<AudioClip>("Sounds/BasicGunSound");
        teamType = user.teamType;

        if (audioClip == null)
            Debug.Log("오디오클립 비었음");

        useCycle = 0.65f;
    }
    public Gun()
    {
        audioClip = Resources.Load<AudioClip>("Sounds/BasicGunSound");

        if (audioClip == null)
            Debug.Log("오디오클립 비었음");

        useCycle = 0.65f;
    }

    public override void Use()
    {
        Fire();
    }


    public override void SetStatus()
    {
        this.user = user;

        audioClip = Resources.Load<AudioClip>("Sounds/BasicGunSound");
        teamType = user.teamType;

        if (audioClip == null)
            Debug.Log("오디오클립 비었음");

        useCycle = 0.65f;
    }

    public override void SetUser(IAttackable unit)
    {

    }


    IEnumerator StartCycle()
    {
        isShootable = false;
        float timeAfterUse = 0;
        while (timeAfterUse <= useCycle)
        {
            timeAfterUse += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isShootable = true;
    }

    public virtual void ChangeLoadedBullet(IShootable bullet)
    {
    }

    public virtual void ChangeLoadedBullet()
    {
    }

    public virtual void Fire()
    {
        if (!isShootable && (weaponSpace == null))
        {
            return;
        }
        if (loadedBullets.Count <= 0)
        {
            Debug.Log(user.name + " 총알 재장전 필요");
            return;
        }

        IShootable currentBullet;
        currentBullet = loadedBullets[0];
        loadedBullets.RemoveAt(0);

        currentBullet.Shoot();

        currentBullet.power += this.power;
        currentBullet.spd = 15;
        currentBullet.power = currentBullet.power + attackableUser.power;


        //loadedBullet.teamType = this.teamType;
        CoroutineHelper.instance.RunCoroutine(StartCycle());

        if (teamType == TeamType.ENEMY)
        {
            return;
        }
        AudioSource.PlayClipAtPoint(audioClip, weaponSpace.transform.position);
        Debug.Log("쏘는중");
    }

    public virtual void Reload(IShootable[] shootables)
    {
        loadedBullets.AddRange(shootables);
    }

}
