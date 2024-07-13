using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour, ITeamMember
{
    public int power { get { return _power; } set { _power = value; } }
    [SerializeField] private int _power = 0;
    Collider collider;

    public TeamType teamType { get { return _teamType; } set{ _teamType = value; } }
    private TeamType _teamType;

    private bool isDisabled;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponentInParent<UnitBase>() != null)
        {
            UnitBase unit = other.transform.GetComponentInParent<UnitBase>();
            if (unit.teamType != _teamType)
            {
                isDisabled = true;
                Vector3 closetPoint = other.ClosestPoint(transform.position);
                unit.Hit(power, closetPoint);
                
                return;
            }
        }
    }

    private void LateUpdate()
    {
        if (isDisabled)
        {
            collider.enabled = false;
        }
    }

}
