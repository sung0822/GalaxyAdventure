using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class UnitBody : MonoBehaviour, ITeamMember
{
    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    private TeamType _teamType;
    public Rigidbody rigidbody;
    public Collider collider;

    UnitBase masterUnit;

    void Awake()
    {
        masterUnit = GetComponentInParent<UnitBase>();
        teamType = masterUnit.teamType;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        UnitBase enemy = CheckBumpedIntoEnemy(other);
        if (enemy == null)
        {
            Debug.Log("적 충돌 유닛이 없음");
        }
        else
        {
            enemy.Hit(30);
        }
    }

    protected UnitBase CheckBumpedIntoEnemy(Collision other)
    {

        if (masterUnit.isBumpedIntoEnemy)
            return null;

        UnitBase unit = other.transform.GetComponentInParent<UnitBase>();

        if (unit == null)
        {
            Debug.Log("부모 오브젝트가 유닛베이스가 아님");
            return null;
        }

        if (unit.teamType != teamType)
        {
            Debug.Log("부딪힘");
            masterUnit.SetIsBumped(true);
            masterUnit.SetIsBumped(false, 3.0f);

            return unit;
        }
        else
        {
            Debug.Log("적이 아님");
            return null;
        }

    }

    protected UnitBase CheckBumpedIntoEnemy(Collider other)
    {
        if (masterUnit.isBumpedIntoEnemy)
        {
            Debug.Log("이미 부딪힌 상태임");
            return null;
        }

        UnitBase unit = other.transform.GetComponentInParent<UnitBase>();

        if (unit == null)
        {
            Debug.Log("부모 오브젝트가 유닛베이스가 아님");
            return null;
        }

        if (unit.teamType != teamType)
        {
            Debug.Log("부딪힘");
            masterUnit.SetIsBumped(true);
            StartCoroutine(masterUnit.SetIsBumped(false, 3.0f));

            return unit;
        }
        else
        {
            Debug.Log("적이 아님");
            return null;
        }

    }

}
