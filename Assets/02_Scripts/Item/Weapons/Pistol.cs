using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : Gun
{
    public override UnitBase user { get; set; }
    public override int id { get { return _id; }}
    private int _id = 0;
    public override float useCycle { get { return _useCycle; } set { _useCycle = value; } }
    protected float _useCycle;
    
    private bool isShootable = true;
    public override TeamType teamType { get; set; }
    public override WeaponSpace weaponSpace { get; set; }
    public override int power { get; set; }
    private int _power = 10;
    public override ItemUsageType usageType { get { return _usageType; } }
    private ItemUsageType _usageType = ItemUsageType.ImmediatelyUse;
    public override List<IShootable> loadedBullets { get { return _loadedBullets; } }   
    public List<IShootable> _loadedBullets = new List<IShootable>();

    public override AudioClip audioClip { get; set; }

    private IShootable currentBullet;


    private float shootCycleValue = 1.5f;


    public Pistol(UnitBase user, IAttackable attackableUser, WeaponSpace weaponSpace) : base(user, attackableUser, weaponSpace)
    {
        //loadedBulletPrefab = Resources.Load<GameObject>("Bullets/BasicBullet");
        //audioSource = unitBase.audioSource;
        audioClip = Resources.Load<AudioClip>("Sounds/BasicGunSound");
        teamType = user.teamType;
        
        if (audioClip == null)
            Debug.Log("오디오클립 비었음");

        useCycle = 0.65f;
    }
    public Pistol()
    {
        //loadedBulletPrefab = Resources.Load<GameObject>("Bullets/BasicBullet");
        //audioSource = unitBase.audioSource;
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

        //loadedBulletPrefab = Resources.Load<GameObject>("Bullets/BasicBullet");
        //audioSource = unitBase.audioSource;
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
    public override void Fire()
    {
        base.Fire();
    }

}
