using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BasicGun : WeaponBase, IShootable
{

    public GameObject bulletPrefab;
    NormalBullet normalBullet;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public override UnitBase user { get; set; }
    public override int id { get { return _id; }}
    protected int _id;
    public override float useCycle { get { return _useCycle; } set { _useCycle = value; } }
    protected float _useCycle;
    protected bool isShootable = true;
    public override TeamType teamType { get; set; }
    public override WeaponSpace weaponSpace { get; set; }
    public override int power { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    protected int _power = 10;
    protected float shootCycleValue = 1.5f;

    public BasicGun()
    {
        bulletPrefab = Resources.Load<GameObject>("Bullets/BasicBullet");
        //audioSource = unitBase.audioSource;
        audioClip = Resources.Load<AudioClip>("Sounds/BasicGunSound");
        
        if (audioClip == null)
        {
            Debug.Log("오디오클립 비었음");
        }

        useCycle = 0.65f;
    }
    public override void Use()
    {
        Shoot();
    }


    public override void SetStatus()
    {
    }

    public override void SetUser(IAttackable unit)
    {
        
    }

    public void Shoot()
    {
        if (!isShootable)
        {
            return;
        }
        GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab, weaponSpace.transform.position, weaponSpace.transform.rotation);

        normalBullet = bullet.GetComponent<NormalBullet>();

        normalBullet.power += this._power;
        normalBullet.spd = 15;
        normalBullet.power = normalBullet.power + attackableUser.power;

        normalBullet.Shoot();
        normalBullet.teamType = this.teamType;
        CoroutineHelper.instance.RunCoroutine(StartCycle());

        if (teamType == TeamType.ENEMY)
        {
            return;
        }
        AudioSource.PlayClipAtPoint(audioClip, weaponSpace.transform.position);
        Debug.Log("쏘는중");

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
}
