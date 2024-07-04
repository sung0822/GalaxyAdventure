using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthEnemy : Enemy
{
    public override bool isAttacking { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    bool _isAttacking;

    bool isInvisible;
    public float visibilityTime;
    public float invisibilityTime;

    MeshRenderer meshRenderer;

    protected override void Start()
    {
        base.Start();
        rewardExp = 10;
        rewardScore = 100;
        lifeTime = 0;
        
        weapons[0].user = this;

    }

    protected override void Update()
    {
        base.Update();

        if (enableAttack)
        {
            Shoot();
        }

        if (lifeTime >= 20.0f)
        {
            Destroy(this.gameObject);
        }
    }


    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //if (other.transform.la) { }
    }

    IEnumerator ChangeVisibility()
    {

        while (true)
        {
            meshRenderer.material.SetFloat("_Mode", 3);

            isInvisible = !isInvisible;

            // 불투명으로 변경
            if (isInvisible)
            {
                Color color = meshRenderer.material.color;
                color.a = 1.0f;

                meshRenderer.material.color = color;

            }   // 투명으로 변경
            else
            {
                Color color = meshRenderer.material.color;
                color.a = 0.0f;

                meshRenderer.material.color = color;

            }
            yield return new WaitForSeconds(0.5f);
        }

         

    }

}
