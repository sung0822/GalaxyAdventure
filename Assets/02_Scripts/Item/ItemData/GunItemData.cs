
using UnityEngine;

[CreateAssetMenu(fileName = "GunItemData", menuName = "Item Data/Gun ItemData", order = 1)]
public class GunItemData : WeaponItemData, IShootable
{
    public override TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    private TeamType _teamType;

    public GameObject projectilePrefab { get { return _projectilePrefab; } set { _projectilePrefab = value; } }
    [SerializeField] private GameObject _projectilePrefab;
    public AudioClip audioClip { get { return _audioClip; } set { _audioClip = value; } }
    [SerializeField] private AudioClip _audioClip;
    public float useCycle { get { return _useCycle; } set { _useCycle = value; } }
    [SerializeField] private float _useCycle;
    

    public override ItemUsageType itemUsageType { get { return ItemUsageType.ImmediatelyUse; } }

    public override bool isUsing { get { return _isUsing; } set { _isUsing = value; } }

    public override UnitBase unitUser { get { return _unitUser; } set { _unitUser = value; } }
    [SerializeField] private UnitBase _unitUser;

    [SerializeField] private bool _isUsing = false;


    public void Shoot()
    {
    }

    public override ItemBase CreateItem()
    {
        return new BasicGun(this);
    }
}