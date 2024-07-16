using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class GunItemBase : WeaponItemBase
{
    protected AudioSource audioSource;
    public GunItemData gunItemData
    {
        get { return _gunItemData; }
        set
        {
            SetData(value);
        }
    }
    protected GunItemData _gunItemData;

    public override void Use()
    {
        if (gunItemData.shooted)
        {
            return;
        }

        CoroutineHelper.instance.RunCoroutine(StartShoot());
        gunItemData.isUsing = true;
        if (gunItemData.shootSound == null) 
        {
            return;
        }
        audioSource.Play();
        Debug.Log(gunItemData.unitUser.name + gunItemData.name + "오디오플레이");
    }
    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        Debug.Log("Awake 호출됨");
    }

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("Start 호출됨");
        audioSource.clip = gunItemData.shootSound;
    }
    protected virtual void Update() 
    {
    }
    public void LevelUp()
    {
        gunItemData.level += 1;
    }
    public void LevelDown() 
    {
        gunItemData.level -= 1;
    }

    public void ChangeBullet(GameObject projectilePrefab)
    {
        gunItemData.projectilePrefab = projectilePrefab;
    }

    protected override void SetData(ItemData itemData)
    {
        base.SetData(itemData);
        _gunItemData = (GunItemData)itemData;
    }
    protected virtual IEnumerator StartShoot()
    {
        gunItemData.gunState.Shoot();
        gunItemData.shooted = true;
        float time = 0;
        while (gunItemData.shooted)
        {
            time += Time.deltaTime;
            if (time >= gunItemData.useCycle)
            {
                gunItemData.shooted = false;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }


}
