using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, ITeamMember, IShootable, IItemAttribute
{
    protected abstract GameObject projectileSelfPrefab { get; set; }
    public int power { get => _power; set => _power = value; }
    protected int _power = 0;
    public float spd { get => _spd; set => _spd = value; }
    protected float _spd = 0;
    
    protected bool isShooting;

    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }

    public ItemType itemType { get { return _itemType; } }
    private ItemType _itemType = ItemType.Projectile;

    public ItemUsageType usageType { get; }
    private ItemUsageType _usageType = ItemUsageType.ImmediatelyUse;

    public UnitBase user { get; set; }

    public abstract int id { get; }

    protected TeamType _teamType;

    protected bool isDestroied;


    protected virtual void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    protected virtual void Update()
    {
        if (isShooting)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _spd, Space.Self);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (isDestroied)
        {
            return;
        }
        if (other.transform.GetComponentInParent<UnitBase>() != null)
        {
            UnitBase unit = other.transform.GetComponentInParent<UnitBase>();
            if (unit.teamType != _teamType)
            {
                Vector3 closetPoint = other.ClosestPoint(transform.position);
                unit.Hit(_power, closetPoint);
                
                Destroy(this.gameObject);

                return;
            }
            else
            {
            }

        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WALL"))
        {
            Destroy(this.gameObject);
            return;
        }
        else if (collision.transform.GetComponentInParent<UnitBase>() != null)
        {
            if (collision.transform.GetComponent<UnitBase>().teamType != teamType)
            {
                collision.transform.GetComponent<UnitBase>().Hit(_power);
                Destroy(this.gameObject);
                return;
            }
            else
            {
            }
        }

        
    }

    public virtual void Shoot()
    {
        Instantiate(projectileSelfPrefab);
        isShooting = true;
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }
}
