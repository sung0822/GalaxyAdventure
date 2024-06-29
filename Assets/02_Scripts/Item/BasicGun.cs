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

    public float shootCycle 
    { get { return _shootCycle; } set { _shootCycle = value; } }

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

        normalBullet.power = 10;
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        //Vector3 worldVelocity = rigidbody.velocity;
        //Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);
        //float localSpeed = localVelocity.magnitude;
        
        normalBullet.spd = 15;

        normalBullet.power = normalBullet.power + user.power;
        normalBullet.Team = _user.Team;

        normalBullet.Shoot();
    }

    
    protected void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Bullets/BasicBullet");
    }

    // Update is called once per frame
    protected void Update()
    {
        timeAfterShoot += Time.deltaTime;
    }

}
