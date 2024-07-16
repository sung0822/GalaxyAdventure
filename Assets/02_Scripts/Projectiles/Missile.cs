using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    Transform spawnPoint;

    Collider collider;

    [SerializeField] GameObject explosion;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip explodingSound;

    [SerializeField] MeshRenderer meshRenderer;

    public float spd = 1;

    public override int power { get { return _power; } set { _power = value; } }
    private int _power;
    private void Awake()
    {
        spawnPoint = GameObject.FindWithTag("ALL_SPAWNPOINTS_GROUP").transform.Find("missileSpawnPoint");
        transform.position = spawnPoint.position;
        transform.LookAt(GameObject.FindWithTag("MAIN_MANAGER").transform);
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
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
        HitBox explosion = Instantiate(this.explosion).GetComponent<HitBox>();
        explosion.transform.position = transform.position;
        explosion.SetEnableCollider(false);
        audioSource.clip = explodingSound;
        //audioSource.Play();
        //Debug.Log("오디오플레이");

        meshRenderer.enabled = false;
        meshRenderer.material.color = Color.clear;

        Destroy(this.gameObject, 3);
    }
}
