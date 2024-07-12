using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Explosion : MonoBehaviour, ITeamMember
{
    Player player;

    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    private TeamType _teamType;
    public int power { get { return _power; } set { _power = value; } }
    int _power = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.transform.root.gameObject;
        UnitBase unit = gameObject.GetComponent<UnitBase>();

        if (unit == null)
        {
            return;
        }

        if (unit.teamType == TeamType.ALLY)
        {
            return;
        }
        unit.DieUnit();
        unit.Hit(power);

        Destroy(this.gameObject, 4.0f);

    }
}
