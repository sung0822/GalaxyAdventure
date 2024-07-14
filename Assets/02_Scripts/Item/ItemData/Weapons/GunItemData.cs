
using UnityEngine;

public abstract class GunItemData : WeaponItemData
{

    public GameObject projectilePrefab { get { return _projectilePrefab; } set { _projectilePrefab = value; } }
    [SerializeField] private GameObject _projectilePrefab;
    public float useCycle { get { return _useCycle; } set { _useCycle = value; } }
    [SerializeField] private float _useCycle;

    public float forceForProjectile { get { return _forceForProjectile; } set { _forceForProjectile = value; } }
    [SerializeField] private float _forceForProjectile = 10;

    AudioSource audioSoruce { get { return _audioSource; } set { _audioSource = value; } }
    [SerializeField] private AudioSource _audioSource;
    public AudioClip shootSound { get { return _shootSound; } set { _shootSound = value; } }
    [SerializeField] AudioClip _shootSound;

    public bool playSondWhileShooting { get { return _playSondWhileShooting; } }
    [SerializeField] protected bool _playSondWhileShooting;

    public bool shooted { get { return _isShooting; } set { _isShooting = value; } }
    [SerializeField] protected bool _isShooting;
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
    

    public override ItemData SetData(ItemData itemData)
    {
        if (itemData is GunItemData)
        {
            GunItemData gunItemData = (GunItemData)itemData;
            base.SetData(itemData);
            projectilePrefab = gunItemData.projectilePrefab;
            useCycle = gunItemData.useCycle;
            forceForProjectile = gunItemData.forceForProjectile;
            shootSound = gunItemData.shootSound;

            return this;
        }
        else
        {
            return null;
        }

    }

}