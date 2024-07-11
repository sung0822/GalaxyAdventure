
using UnityEngine;

public abstract class GunItemData : WeaponItemData
{
    public override TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    private TeamType _teamType;

    public GameObject projectilePrefab { get { return _projectilePrefab; } set { _projectilePrefab = value; } }
    [SerializeField] private GameObject _projectilePrefab;
    public AudioClip audioClip { get { return _audioClip; } set { _audioClip = value; } }
    [SerializeField] private AudioClip _audioClip;
    public float useCycle { get { return _useCycle; } set { _useCycle = value; } }
    [SerializeField] private float _useCycle;

    public float forceForProjectile { get { return _forceForProjectile; } set { _forceForProjectile = value; } }
    [SerializeField] private float _forceForProjectile = 10;

    public IGunState gunState { get { return _gunState; } }
    public IGunState _gunState;
    public override int level { get { return _level; } set { SetLevel(value); } }
    
    public void SetLevel(int value)
    {
        _level = value;
        switch (_level) 
        {
            case 1:
                _gunState = new GunLevelOneState(this);
                
            break;
            case 2:
                _gunState = new GunLevelTwoState(this);

                break;
            case 3:
                _gunState = new GunLevelThreeState(this);
                
            break;
            default:

                break;
        }
    }
    public override ItemUsageType itemUsageType { get { return _itemUsageType; } }
    [SerializeField] ItemUsageType _itemUsageType;

    public override bool isUsing { get { return _isUsing; } set { _isUsing = value; } }

    public override UnitBase unitUser { get { return _unitUser; } set { _unitUser = value; } }
    [SerializeField] private UnitBase _unitUser;

    [SerializeField] private bool _isUsing = false;

}