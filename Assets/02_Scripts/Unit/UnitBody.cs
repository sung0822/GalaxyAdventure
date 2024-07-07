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
            Debug.Log("�� �浹 ������ ����");
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
            Debug.Log("�θ� ������Ʈ�� ���ֺ��̽��� �ƴ�");
            return null;
        }

        if (unit.teamType != teamType)
        {
            Debug.Log("�ε���");
            masterUnit.SetIsBumped(true);
            masterUnit.SetIsBumped(false, 3.0f);

            return unit;
        }
        else
        {
            Debug.Log("���� �ƴ�");
            return null;
        }

    }

    protected UnitBase CheckBumpedIntoEnemy(Collider other)
    {
        if (masterUnit.isBumpedIntoEnemy)
        {
            Debug.Log("�̹� �ε��� ������");
            return null;
        }

        UnitBase unit = other.transform.GetComponentInParent<UnitBase>();

        if (unit == null)
        {
            Debug.Log("�θ� ������Ʈ�� ���ֺ��̽��� �ƴ�");
            return null;
        }

        if (unit.teamType != teamType)
        {
            Debug.Log("�ε���");
            masterUnit.SetIsBumped(true);
            StartCoroutine(masterUnit.SetIsBumped(false, 3.0f));

            return unit;
        }
        else
        {
            Debug.Log("���� �ƴ�");
            return null;
        }

    }

}
