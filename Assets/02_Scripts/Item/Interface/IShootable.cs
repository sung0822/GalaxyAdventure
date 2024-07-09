using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    public GameObject projectilePrefab { get; set; }

    public AudioClip audioClip { get; set; }
    public float useCycle { get; set; }
    public void Shoot();

}
