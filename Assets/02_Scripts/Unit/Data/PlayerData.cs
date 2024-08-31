using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "UnitData/UnitBaseData/PlayerData", order = 1)]
public class PlayerData : UnitBaseData
{
    public GameObject currentAircraft { get { return _currentAircraft; } set { _currentAircraft = value; } }
    [Header("PlayerData")]
    [SerializeField] protected GameObject _currentAircraft;

    public GameObject previousAircraft { get { return _previousAircraft; } set { _previousAircraft = value; } }
    [SerializeField] protected GameObject _previousAircraft;

    public GameObject hydroBeamPrefab { get { return _hydroBeamPrefab; } set { _hydroBeamPrefab = value; } }
    [SerializeField] protected GameObject _hydroBeamPrefab;

    public AudioClip changeAirCraftSound { get { return _changeAirCraftSound; } set { _changeAirCraftSound = value; } }
    [SerializeField] protected AudioClip _changeAirCraftSound;
    public AudioClip changeBulletSound { get { return _changeBulletSound; } set { _changeBulletSound = value; } }
    [SerializeField] protected AudioClip _changeBulletSound;
    public PlayerLevelUpData playerLevelUpData { get { return _playerLevelUpData; } set { _playerLevelUpData = value; } }
    [SerializeField] private PlayerLevelUpData _playerLevelUpData;
    public List<int> maxHpPerLevel { get { return _maxHpPerLevel; } set { _maxHpPerLevel = value; } }
    [SerializeField] protected List<int> _maxHpPerLevel = new List<int>();
    public bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    protected bool _isAttacking;
    public int currentLevel { get { return _currentLevel; } set { _currentLevel = value; } }
    [SerializeField] int _currentLevel;
    public float currentExp { get { return _currentExp; } set { _currentExp = value; } }
    [SerializeField] float _currentExp;
    public float currentExpToLevel { get { return _currentExpToLevel; } set { _currentExpToLevel = value; } }
    [SerializeField] float _currentExpToLevel;
    public int power { get { return _power; } set { _power = value; } }
    [SerializeField] public int _power = 10;
    public float currentAbilityGage { get { return _currentAbilityGage; } set { _currentAbilityGage = value; } }
    [SerializeField] float _currentAbilityGage;
    public float maxAbilityGage { get { return _maxAbilityGage; } set { _maxAbilityGage = value; } }
    [SerializeField] float _maxAbilityGage;
    public bool isInvincibilityBlinking { get { return _isInvincibilityBlinking; } set { _isInvincibilityBlinking = value; } }
    [SerializeField] bool _isInvincibilityBlinking;
    
    public float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField] protected float _moveSpd;
    public bool isAbsoluteImmortal { get { return _isAbsoluteImmortal; } set { _isAbsoluteImmortal = value; } }
    [SerializeField] protected bool _isAbsoluteImmortal;

    public override void SetData(UnitBaseData unitBaseData)
    {
        base.SetData(unitBaseData);

        PlayerData playerData = (PlayerData)unitBaseData;


        for (int i = 0; i < playerData.maxHpPerLevel.Count; i++)
        {
            this._maxHpPerLevel[i] = playerData.maxHpPerLevel[i];
        }
        this._currentAircraft = playerData.currentAircraft;
        this._previousAircraft = playerData.previousAircraft;
        this._hydroBeamPrefab = playerData.hydroBeamPrefab;
        this._changeAirCraftSound = playerData.changeAirCraftSound;
        this._changeBulletSound = playerData.changeBulletSound;

        this._isAttacking = playerData.isAttacking;
        this._currentLevel = playerData.currentLevel;
        this._currentExp = playerData.currentExp;
        this._playerLevelUpData = playerData.playerLevelUpData;
        this._currentExpToLevel = playerData.currentExpToLevel;
        this._power = playerData.power;
        this._currentAbilityGage = playerData.currentAbilityGage;
        this._maxAbilityGage = playerData.maxAbilityGage;
        this._isInvincibilityBlinking = playerData.isInvincibilityBlinking;
        this._moveSpd = playerData.moveSpd;
        this._isAbsoluteImmortal = playerData.isAbsoluteImmortal;
    }
}
