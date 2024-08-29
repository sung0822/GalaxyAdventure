using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitBaseData", menuName = "UnitData/UnitBaseData", order = 1)]
public class UnitBaseData : ScriptableObject
{
    protected AudioClip dieSound { get { return _dieSound; } set { _dieSound = value; } }
    [SerializeField] AudioClip _dieSound;
    
    public int currentMaxHp { get { return _currentMaxHp; } set { _currentMaxHp = value; } }
    [SerializeField] protected int _currentMaxHp;

    public int currentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            if (_currentHp > currentMaxHp)
            {
                _currentHp = currentMaxHp;
            }
        }
    }
    [SerializeField] protected int _currentHp;

    public bool isImmortal { get { return _isImmortal; } }
    [SerializeField] protected bool _isImmortal;
    
    public bool isBumpedIntoEnemy { get { return _isBumpedIntoEnemy; } }
    [SerializeField] protected bool _isBumpedIntoEnemy;

    public bool isDead { get { return _isDead; } set { _isDead = value; } }
    [SerializeField] protected bool _isDead = false;
    
    public string unitName { get { return _unitName; } set { _unitName = value; } }
    [SerializeField] protected string _unitName;
    
    public bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    [SerializeField] protected bool _isAttacking;

    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    [SerializeField] TeamType _teamType;
}