using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eagle : Enemy
{
    public override bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    private bool _isAttacking;

    Transform middleTransform;
    float time = 0;


    protected override void Start()
    {
        base.Start();
        rewardExp = 10;
        rewardScore = 100;
        lifeTime = 0;

        currentHp = 10;
        maxHp = 10;

        currentPattern = patterns[1];

        transform.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

    }

    protected override void Update()
    {
        base.Update();

        time += Time.deltaTime;
        currentPattern.Execute();

    }


    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void Die()
    {
        targetPlayer.GivePlayerExp(rewardExp);
        MainManager.Get().score += rewardScore;
        UIManager.instance.CheckScore();


        GameObject item = ItemManager.instance.MakeItem(transform);
        
        item.GetComponent<ItemComponent>().transform.Rotate(-50, 0, 0);
        
        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.eagleDieParticle, transform.position, transform.rotation);

        particle.transform.localScale = this.transform.localScale * 0.1f;
        Destroy(particle, 1.5f);

        Destroy(this.gameObject);
    }
    void Shoot()
    {
        if (time > 1.0f)
        {
            weapons[0].Use();
            time = 0;
        }
    }
}
