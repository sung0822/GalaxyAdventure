using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour, IWeapon
{
    public GameObject bulletPrefab;
    NormalBullet normalBullet;
    
    public Transform fireTransform;

    public Unit user {  get { return _user; }
                        set { _user = value; } }

    Unit _user;

    public void Use()
    {
        GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        normalBullet = bullet.GetComponent<NormalBullet>();
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        normalBullet.power = 10;
        normalBullet.spd = 10;
        normalBullet.power = normalBullet.power + user.power;

        normalBullet.Shoot();
    }

    
    protected void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Bullets/BasicBullet");
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

}
