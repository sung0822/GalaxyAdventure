using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData", order = int.MaxValue)]
public abstract class UnitData : ScriptableObject
{
    public List<UnitBody> unitBodyColliders = new List<UnitBody>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public List<Collider> colliders = new List<Collider>();
    public Rigidbody unitRigidbody;
    public int maxHp { get; set; }
    public int currentHp { get; set; }
    public bool isImmortal { get { return _isImmortal; } }
    protected bool _isImmortal;
    public bool isBumpedIntoEnemy { get { return _isBumpedIntoEnemy; } }
    private bool _isBumpedIntoEnemy;

    protected bool isDie = false;
    public bool isAttacking { get; set; }
    public TeamType teamType { get; set; }

}
