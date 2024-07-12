using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
            Debug.Log("ºÎ¸ð ¿ÀºêÁ§Æ®°¡ À¯´Öº£ÀÌ½º°¡ ¾Æ´Ô");
            return null;
        }

        if (unit.teamType != teamType)
        {
            Debug.Log("ºÎµúÈû");
            masterUnit.SetIsBumped(true);
            masterUnit.SetIsBumped(false, 3.0f);

            return unit;
        }
        else
        {
            Debug.Log("ÀûÀÌ ¾Æ´Ô");
            return null;
        }

    }

    protected UnitBase CheckBumpedIntoEnemy(Collider other)
    {
        if (masterUnit.isBumpedIntoEnemy)
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
            masterUnit.SetIsBumped(true);
            StartCoroutine(masterUnit.SetIsBumped(false, 3.0f));

            return unit;
        }
        else
        {
            return null;
        }

    }

}
