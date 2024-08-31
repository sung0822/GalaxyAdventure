using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitBaseData", menuName = "UnitData/UnitBaseData", order = 1)]
public class UnitBaseData : ScriptableObject
{
    public AudioClip dieSound { get { return _dieSound; } set { _dieSound = value; } }
    [Header ("UnitBaseData")]
    [SerializeField] AudioClip _dieSound;
    public GameObject unitDieParticlePrefab { get { return _unitDieParticle; } set { _unitDieParticle = value; } }
    [SerializeField] private GameObject _unitDieParticle;
    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    [SerializeField] TeamType _teamType;
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

    public bool isImmortal { get { return _isImmortal; } set { _isImmortal = value; } }
    [SerializeField] protected bool _isImmortal;
    
    public bool isBumpedIntoEnemy { get { return _isBumpedIntoEnemy; } set { _isImmortal = value; } }
    [SerializeField] protected bool _isBumpedIntoEnemy;

    public bool isDead { get { return _isDead; } set { _isDead = value; } }
    [SerializeField] private bool _isDead = false;
    
    public string unitName { get { return _unitName; } set { _unitName = value; } }
    [SerializeField] protected string _unitName;


    virtual public void SetData(UnitBaseData unitBaseData)
    {
        this._dieSound = unitBaseData.dieSound;

        this._currentHp = unitBaseData.currentHp;
        this._currentMaxHp = unitBaseData.currentMaxHp;

        this._isImmortal = unitBaseData.isImmortal;
        this._isBumpedIntoEnemy = unitBaseData.isBumpedIntoEnemy;

        this._unitName = unitBaseData.unitName;

        this._teamType = unitBaseData.teamType;
    }
}