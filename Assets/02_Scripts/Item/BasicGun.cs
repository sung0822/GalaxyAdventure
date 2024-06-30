using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicGun : MonoBehaviour, IWeapon
{
    public GameObject bulletPrefab;
    NormalBullet normalBullet;

    public AudioSource audioSource;
    public AudioClip audioClip;
    
    public Transform fireTransform;

    public Unit user {  get { return _user; }
                        set { _user = value; } }

    public float shootCycle 
    { get { return _shootCycle; } set { _shootCycle = value; } }

    public int power { get; set; }
    protected int _power = 10;

    protected float _shootCycle = 1.5f;
    protected float timeAfterShoot;

    Unit _user;

    public void Use()
    {
        if (timeAfterShoot <= _shootCycle)
        {
            return;
        }
        timeAfterShoot = 0;
        
        GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab, transform.position, transform.rotation);
        
        normalBullet = bullet.GetComponent<NormalBullet>();
        
        normalBullet.power += this._power;
        normalBullet.spd = 15;
        normalBullet.power = normalBullet.power + user.power;
        normalBullet.Team = _user.Team;
        normalBullet.Shoot();
        
        if (_user.Team == TeamType.ENEMY)
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

    // Update is called once per frame
    protected void Update()
    {
        timeAfterShoot += Time.deltaTime;
    }

}
