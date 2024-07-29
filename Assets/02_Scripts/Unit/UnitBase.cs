using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

[RequireComponent(typeof(Rigidbody))]
public abstract class UnitBase : MonoBehaviour, ITeamMember
{
    protected AudioClip dieSound { get { return _dieSound; } set { _dieSound = value; } }
    [SerializeField] AudioClip _dieSound;
    public List<UnitBody> unitBodyColliders = new List<UnitBody>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public List<Collider> colliders = new List<Collider>();
    public Rigidbody unitRigidbody;
    public int currentMaxHp { get { return _currentMaxHp; } set { _currentMaxHp = value; } }
    [SerializeField] protected int _currentMaxHp;
    public int currentHp 
    { 
        get { return _currentHp; }
        set 
        {
            _currentHp = value;
            if (_currentHp > currentMaxHp)
            {
                _currentHp = currentMaxHp;
            }
        } 
    }
    [SerializeField] protected int _currentHp;
    public bool isImmortal { get { return _isImmortal; }}
    protected bool _isImmortal;
    public bool isBumpedIntoEnemy { get { return _isBumpedIntoEnemy; } }
    private bool _isBumpedIntoEnemy;

    public bool isDead { get { return _isDead; } set { _isDead = value; } }
    protected bool _isDead = false;
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
        colliders.AddRange(GetComponentsInChildren<Collider>());
        dieSound = Resources.Load<AudioClip>("Sounds/ExplosionSound");
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
        UnitBase enemy = CheckBumpedIntoEnemy(other);
        if (enemy == null)
        {
            
        }
        else
        {
            enemy.Hit(30);
        }
    }


    /// <summary> 해당 콜라이더가 적 유닛인지 체크, 충돌변수를 설정함. </summary>
    protected UnitBase CheckBumpedIntoEnemy(Collider other)
    {
        
        if (isBumpedIntoEnemy)
        {
            return null;
        }
        if (other.tag == "HYDRO_BEAM")
        {
        
            return null;
        }
        UnitBase unit = other.transform.GetComponentInParent<UnitBase>();

        if (unit == null)
        {
            return null;
        }

        if (unit.teamType != teamType)
        {
            SetIsBumped(true);
            StartCoroutine(SetIsBumped(false, 3.0f));

            return unit;
        }
        else
        {
            
            return null;
        }

    }
    protected UnitBase CheckBumpedIntoEnemy(Collision other)
    {

        if (isBumpedIntoEnemy)
        {
            return null;
        }

        UnitBase unit = other.transform.GetComponentInParent<UnitBase>();

        if (unit == null)
        {
            return null;
        }

        if (unit.teamType != teamType)
        {
            SetIsBumped(true);
            StartCoroutine(SetIsBumped(false, 3.0f));

            return unit;
        }
        else
        {
            return null;
        }

    }

    protected virtual void OnCollisionStay(Collision collision)
    {
    }

    public virtual void Hit(int damage)
    {
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
            return;
        
        currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        CheckDead();
    }

    public IEnumerator SetIsBumped(bool isBumpedIntoEnemy, float time)
    {
        yield return new WaitForSeconds(time);
        this._isBumpedIntoEnemy = isBumpedIntoEnemy;
    }

    public void SetIsBumped(bool isBumpedIntoEnemy)
    {
        this._isBumpedIntoEnemy = isBumpedIntoEnemy;
    }

    Coroutine immortalCoroutine = null;
    public virtual void SetImmortalDuring(bool isImmortal, float time)
    {
        if (this.isImmortal)
        {
            StopCoroutine(immortalCoroutine);
        }
        immortalCoroutine = StartCoroutine(SetImmortalCoroutine(isImmortal, time));
    }
    protected IEnumerator SetImmortalCoroutine(bool isImmortal, float time)
    {
        this._isImmortal = isImmortal;
        yield return new WaitForSeconds(time);
        this._isImmortal = !isImmortal;
    }
    public virtual void SetImmortal(bool isImmortal)
    {
        this._isImmortal = isImmortal;
    }

    protected virtual bool CheckDead()
    {
        if (currentHp <= 0)
        {
            DieUnit();
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary> 오브젝트 파괴하고 파티클 생성 </summary>
    public virtual void DieUnit()
    {
        isDead = true;
        
        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.unitExplodingParticle, transform.position, transform.rotation);

        particle.transform.localScale = this.transform.localScale * 0.1f;
        Destroy(particle, 1.5f);

        Destroy(this.gameObject);
    }

    protected virtual void LateUpdate()
    {
    }

}
