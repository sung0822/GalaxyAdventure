using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : BaseWeapon
{
    public GameObject bulletPrefab;
    NormalBullet normalBullet;

    public Transform fireTransform;

    public override void Use()
    {
        GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab);
        normalBullet = bullet.GetComponent<NormalBullet>();
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        
        normalBullet.Shoot();
        normalBullet.power = 10;
    }

    
    protected override void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

}
