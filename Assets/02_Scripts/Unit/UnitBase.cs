using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

[RequireComponent(typeof(Rigidbody))]
public abstract class UnitBase : MonoBehaviour, ITeamMember
{
    public List<UnitBody> unitBodyColliders = new List<UnitBody>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public List<Collider> colliders = new List<Collider>();
    public Rigidbody unitRigidbody;
    public abstract TeamType teamType { get; set; }

    
    [SerializeField] private UnitBaseData unitBaseData;
    public UnitBaseData currentUnitBaseData { get { return _currentUnitBaseData; } }
    [SerializeField] protected UnitBaseData _currentUnitBaseData;


    protected virtual void Awake()
    {
        _currentUnitBaseData = ScriptableObject.Instantiate(unitBaseData);
    }

    protected virtual void Start()
    {
        unitRigidbody = GetComponent<Rigidbody>();
        transform.parent = null;

        SetFirstStatus();
    }

    protected virtual void OnEnable()
    {
        SetFirstStatus();
    }

    /// <summary>
    /// 모든 유닛은 시작시 스탯을 정리하고 가야함.
    /// </summary>
    protected virtual void SetFirstStatus()
    {
        _currentUnitBaseData.SetData(unitBaseData);
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
            Debug.Log("enemy가 null임");
        }
        else
        {
            Debug.Log("enemy Hit");
            enemy.Hit(30);
        }
    }


    /// <summary> 해당 콜라이더가 적 유닛인지 체크, 충돌변수를 설정함. </summary>
    protected UnitBase CheckBumpedIntoEnemy(Collider other)
    {
        
        if (_currentUnitBaseData.isBumpedIntoEnemy)
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

        if (unit.teamType != _currentUnitBaseData.teamType)
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

        if (_currentUnitBaseData.isBumpedIntoEnemy)
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
        if (_currentUnitBaseData.isImmortal)
        {
            return;
        }
        Debug.Log("unitHit is called, and unit Name is: " + currentUnitBaseData.unitName);
        _currentUnitBaseData.currentHp -= damage;
        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, this.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        
        CheckDead();
    }

    public virtual void Hit(int damage, Transform hitTransform)
    {
        if (_currentUnitBaseData.isImmortal)
            return;

        Debug.Log("unitHit is called, and unit Name is: " + currentUnitBaseData.unitName);
        _currentUnitBaseData.currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, hitTransform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        
        CheckDead();
    }

    public virtual void Hit(int damage, Vector3 position)
    {

        if (_currentUnitBaseData.isImmortal)
            return;

        Debug.Log("unitHit is called, and unit Name is: " + currentUnitBaseData.unitName);
        _currentUnitBaseData.currentHp -= damage;

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);
        CheckDead();
    }

    public IEnumerator SetIsBumped(bool isBumpedIntoEnemy, float time)
    {
        yield return new WaitForSeconds(time);
        _currentUnitBaseData.isBumpedIntoEnemy = isBumpedIntoEnemy;
    }

    public void SetIsBumped(bool isBumpedIntoEnemy)
    {
        _currentUnitBaseData.isBumpedIntoEnemy = isBumpedIntoEnemy;
    }

    Coroutine immortalCoroutine = null;
    public virtual void SetImmortalDuring(bool isImmortal, float time)
    {
        if (_currentUnitBaseData.isImmortal)
        {
            StopCoroutine(immortalCoroutine);
        }
        immortalCoroutine = StartCoroutine(SetImmortalCoroutine(isImmortal, time));
    }
    protected IEnumerator SetImmortalCoroutine(bool isImmortal, float time)
    {
        Debug.Log("Set Immortal during is called");
        _currentUnitBaseData.isImmortal = isImmortal;
        yield return new WaitForSeconds(time);
        _currentUnitBaseData.isImmortal = !isImmortal;
    }
    public virtual void SetImmortal(bool isImmortal)
    {
        _currentUnitBaseData.isImmortal = isImmortal;
    }

    protected virtual bool CheckDead()
    {
        if (_currentUnitBaseData.currentHp <= 0)
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
        _currentUnitBaseData.isDead = true;

        GameObject particle = ParticleManager.instance.CreateParticle(currentUnitBaseData.unitDieParticlePrefab, transform.position, transform.rotation);
        if (particle == null)
        {
            Debug.Log("particle이 null임");
        }
        else if (this == null)
        {
            Debug.Log("this가 null임");
        }
        particle.transform.localScale = this.transform.localScale * 0.1f;
        


        Destroy(particle, 1.5f);

        ObjectPoolManager.instance.ReturnObject(currentUnitBaseData.unitName + " Pool", this.gameObject);
    }

    protected virtual void LateUpdate()
    {
    }

}
