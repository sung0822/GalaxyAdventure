using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Unit
{

    protected List<WeaponBase> weapons = new List<WeaponBase>();
    protected List<IPattern> patterns = new List<IPattern>();
    
    protected IPattern currentPattern;
    protected IPlayer targetPlayer;

    protected int rewardExp;
    protected int rewardScore;

    public bool enableSlow = false;
    public bool enableAttack = false;

    public float lifeTime;


    protected override void Start()
    {
        base.Start();
        _myTeam = TeamType.ENEMY;

        patterns.Add(new Pattern1());
        patterns.Add(new RotatingPattern());

        for(int i = 0; i < patterns.Count; i++)
        {
            patterns[i].SetTarget(transform);
        }

        currentPattern = patterns[0];

        targetPlayer = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Player>();
        transform.SetParent(null);

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
        
        //enableSlow가 true라면 벽 충돌시 느려짐
        if (other.GetComponentInParent<Rigidbody>()?.transform.tag == "WALL")
        {
            enableAttack = true;
            
            if (!enableSlow)
            {
                return;
            }
            
            currentPattern.AdjustSpeed(4.0f, 0.5f);
        }
    }

    public override void Hit(int damage)
    {
        base.Hit(damage);
    }

    protected override void Die()
    {
        targetPlayer.GivePlayerExp(rewardExp);
        MainManager.Get().score += rewardScore;
        UIManager.instance.CheckScore();


        GameObject item = ItemManager.instance.MakeItem(transform);
        item.GetComponent<ItemComponent>().transform.Rotate(-50, 0, 0);
        //Instantiate<GameObject>(Resources.Load<GameObject>("Items/Box"), transform.position, Quaternion.Euler(0, 0, 0));

        base.Die();

    }

    public virtual void ChangePattern(int idx)
    {
        currentPattern = patterns[idx];
    }

}
