using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    Transform spawnPoint;

    Collider collider;

    [SerializeField] GameObject explosion;

    public float spd = 1;

    public override int power { get { return _power; } set { _power = value; } }
    private int _power;

    private void Awake()
    {
        spawnPoint = GameObject.FindWithTag("ALL_SPAWNPOINTS_GROUP").transform.Find("missileSpawnPoint");
        transform.position = spawnPoint.position;
        transform.LookAt(GameObject.FindWithTag("MAIN_MANAGER").transform);
        collider = GetComponent<Collider>();
    }

    protected override void Update()
    {
        transform.Translate(Vector3.forward * spd * Time.deltaTime, Space.Self);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLANE")
        {
            Explode();
        }
    }

    void Explode()
    {
        Explosion explosion = Instantiate(this.explosion).GetComponent<Explosion>();
        explosion.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
