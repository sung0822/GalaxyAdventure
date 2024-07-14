using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour, ITeamMember
{
    public int power { get { return _power; } set { _power = value; } }
    [SerializeField] private int _power = 0;
    Collider collider;

    public TeamType teamType { get { return _teamType; } set{ _teamType = value; } }
    private TeamType _teamType;


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
                Vector3 closetPoint = other.ClosestPoint(transform.position);
                unit.Hit(power, closetPoint);

                return;
            }
        }
    }

    private void LateUpdate()
    {
    }

    public void SetEnableCollider(bool enable, float time)
    {
        StartCoroutine(SetEnableColliderAfterTime(enable, time));
    }
    public void SetEnableCollider(bool enable)
    {
        if (collider == null)
        {
            return;
        }
        collider.enabled = enable;
    }
    IEnumerator SetEnableColliderAfterTime(bool enable, float time)
    {
        yield return new WaitForSeconds(time);
        collider.enabled = enable;
    }


}
