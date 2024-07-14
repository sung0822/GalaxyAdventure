using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydroBeam : MonoBehaviour
{
    [SerializeField] HitBox hitBox;
    [SerializeField] Collider collider;
    
    public int power { get { return _power; } set { _power = value; } }
    [SerializeField] int _power;
    [SerializeField] float hitTick;

    public IAttackable attackableUser { get { return _attackableUser; } set { _attackableUser = value; } }

    private IAttackable _attackableUser;

    private void Awake()
    {
    }
    private void Start()
    {
        collider = GetComponent<Collider>();
        hitBox.power += power;
        StartCoroutine(StartHitBoxCycle());
    }

    IEnumerator StartHitBoxCycle()
    {
        while (true) 
        {
            hitBox.SetEnableCollider(false);
            yield return new WaitForFixedUpdate();
            hitBox.SetEnableCollider(true);
            yield return new WaitForSeconds(hitTick);
        }

    }

}
