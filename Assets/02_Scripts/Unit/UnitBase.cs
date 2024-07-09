using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

/// <summary>
/// 어트리뷰트 문법을 잘 몰라서 공부해야할듯. 기능은 해당 컴포넌트를 게임오브젝트가 가지도록 강제한다.
/// </summary>
[RequireComponent (typeof (AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public abstract class UnitBase : MonoBehaviour, ITeamMember
{
    public AudioSource audioSource;
    protected AudioClip audioClip;
    
    public List<UnitBody> unitBodyColliders = new List<UnitBody>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public List<Collider> colliders = new List<Collider>();
    public Rigidbody unitRigidbody;
    public abstract int maxHp { get; set; }
    public abstract int currentHp { get; set; }
    public bool isImmortal { get { return _isImmortal; }}
    protected bool _isImmortal;
    public bool isBumpedIntoEnemy { get { return _isBumpedIntoEnemy; } }
    private bool _isBumpedIntoEnemy;

    protected bool isDie = false;
    public abstract bool isAttacking { get; set; }
    public abstract TeamType teamType { get; set; }

    protected virtual void Start()
    {
        SetFirstStatus();
    }

    /// <summary>
    /// 모든 유닛은 시작시 스탯을 정리하고 가야함.
    /// </summary>
    protected virtual void SetFirstStatus()
    {
        unitRigidbody = GetComponent<Rigidbody>();

        transform.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Sounds/ExplosionSound");
        SetBody();
    }
    protected void SetBody()
    {
        unitBodyColliders.AddRange(GetComponentsInChildren<UnitBody>());
        int count = unitBodyColliders.Count;
        for (int i = 0; i < count; i++)
        {
            rigidbodies.Add(unitBodyColliders[i].rigidbody);
            colliders.Add(unitBodyColliders[i].collider);
        }

        for (int i = count; i > 0; i--)
        {
            for (int j = 0; j < i - 1; j++)
            {
                Physics.IgnoreCollision(colliders[i - 1], colliders[j]);
            }
        }
    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
    }

    

    protected virtual void OnTriggerEnter(Collider other)
    { 
    }


    /// <summary> 해당 콜라이더가 적 유닛인지 체크, 충돌변수를 설정함. </summary>
    protected UnitBase CheckBumpedIntoEnemy(Collider other)
    {
        UnitBase unit = other.transform.GetComponentInParent<UnitBase>();
        
        if (unit == null)
        {
            return null;

        }
        
        if (unit.teamType != teamType)
        {
            SetIsBumped(false, 3.0f);
            return unit;
        }
        else
            return null;

    }
    protected UnitBase CheckBumpedIntoEnemy(Collision other)
    {
        UnitBase unit = other.transform.GetComponentInParent<UnitBase>();

        if (unit == null)
        {
            return null;
        }

        if (unit.teamType != teamType)
        {
            SetIsBumped(false, 3.0f);
            return unit;
        }
        else
            return null;

    }

    protected virtual void OnCollisionStay(Collision collision)
    {
    }

    public virtual void Hit(int damage)
    {
        if (isDie)
        {
            return;
        }
        if (isImmortal)
        {
            return;
        }
        currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, this.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        
        CheckDead();
    }

    public virtual void Hit(int damage, Transform hitTransform)
    {
        if (isImmortal)
            return;

        currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, hitTransform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        
        CheckDead();
    }

    public virtual void Hit(int damage, Vector3 position)
    {

        if (isImmortal)
        {
            return;
        }
        currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        CheckDead();
        Debug.Log("죽음 체크");
    }

    public IEnumerator SetIsBumped(bool isBumpedIntoEnemy, float time)
    {
        yield return new WaitForSeconds(time);
        this._isBumpedIntoEnemy = isBumpedIntoEnemy;
        Debug.Log("isBumpedIntoEnemy: " + _isBumpedIntoEnemy);
    }
    public void SetIsBumped(bool isBumpedIntoEnemy)
    {
        this._isBumpedIntoEnemy = isBumpedIntoEnemy;
    }

    public virtual IEnumerator SetImmortal(bool isImmortal, float time)
    {
        yield return new WaitForSeconds(time);
        this._isImmortal = isImmortal;
    }
    public virtual void SetImmortal(bool isImmortal)
    {
        this._isImmortal = isImmortal;
    }

    protected virtual bool CheckDead()
    {
        if (currentHp <= 0)
        {
            audioSource.clip = audioClip;
            audioSource.enabled = true;
            audioSource.PlayOneShot(audioClip, 1.0f);
            DieUnit();
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary> 오브젝트 파괴하고 파티클 생성 </summary>
    protected virtual void DieUnit()
    {
        isDie = true;
        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.unitExplodingParticle, transform.position, transform.rotation);

        particle.transform.localScale = this.transform.localScale * 0.1f;
        Destroy(particle, 1.5f);

        Destroy(this.gameObject);
    }

    protected virtual void LateUpdate()
    {
    }

}
