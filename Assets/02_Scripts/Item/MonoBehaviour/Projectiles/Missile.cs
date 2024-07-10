using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Transform spawnPoint;

    Collider collider;

    GameObject explosion;

    public float spd = 1;

    private void Awake()
    {
        spawnPoint = GameObject.FindWithTag("ALL_SPAWNPOINTS_GROUP").transform.Find("missileSpawnPoint");
        transform.position = spawnPoint.position;
        transform.LookAt(GameObject.FindWithTag("MAIN_MANAGER").transform);
        collider = GetComponent<Collider>();
        explosion = Resources.Load<GameObject>("Bullets/Explosion");
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * spd * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLANE")
        {
            Explode();
        }


    }

    void Explode()
    {
        GameObject explosion = Instantiate(this.explosion);
        explosion.transform.position = transform.position; 
        Destroy(this.gameObject);
    }
}
