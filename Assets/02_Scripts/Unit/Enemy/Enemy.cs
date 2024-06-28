using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Unit
{

    protected List<IWeapon> weapons = new List<IWeapon>();
    protected List<IPattern> patterns = new List<IPattern>();
    
    protected IPattern currentPattern;
    protected IPlayer targetPlayer;

    protected int rewardExp;
    protected int rewardScore;

    protected override void Start()
    {
        base.Start();
        _myTeam = TeamType.ENEMY;

        patterns.Add(new Pattern1());
        patterns.Add(new Pattern2());

        for(int i = 0; i < patterns.Count; i++)
        {
            patterns[i].SetTarget(transform);
        }

        currentPattern = patterns[0];

        targetPlayer = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<PlayerCtrl>();

    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void Hit(int damage)
    {
        base.Hit(damage);
    }

    protected override void Die()
    {
        targetPlayer.GivePlayerExp(rewardExp);
        MainManager.Get().score += rewardScore;
        base.Die();

    }

    public virtual void ChangePattern(int idx)
    {
        currentPattern = patterns[idx];
    }

}
