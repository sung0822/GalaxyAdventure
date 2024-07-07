using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BasicGun : WeaponBase
{
    public GameObject bulletPrefab;
    NormalBullet normalBullet;

    public AudioSource audioSource;
    public AudioClip audioClip;

    public override float useCycle { get { return _useCycle; } set { _useCycle = value; } }

    public override TeamType teamType { get; set; }

    protected float _useCycle;

    protected int _power = 10;

    protected float shootCycleValue = 1.5f;
    protected float timeAfterShoot;

    public override void Use()
    {
        if (timeAfterShoot <= _useCycle)
        {
            return;
        }
        timeAfterShoot = 0;
        
        GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab, transform.position, transform.rotation);
        
        normalBullet = bullet.GetComponent<NormalBullet>();
        
        normalBullet.power += this._power;
        normalBullet.spd = 15;
        normalBullet.power = normalBullet.power + user.power;

        normalBullet.Shoot();
        normalBullet.teamType = this.teamType;

        if (teamType == TeamType.ENEMY)
        {
            return;
        }
        audioSource.PlayOneShot(audioClip);
    }

    
    protected void Start()
    {
        timeAfterShoot = 0;
        bulletPrefab = Resources.Load<GameObject>("Bullets/BasicBullet");
        audioClip = Resources.Load<AudioClip>("Sounds/BasicGunSound");
        audioSource = transform.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.volume = audioSource.volume / 2;
        
    }
    protected void Update()
    {
        timeAfterShoot += Time.deltaTime;
    }

    public override void SetUser(IAttackable unit)
    {
        
    }
}
