using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    public int power { get; set; }
    public float spd { get; set; }

    public void Shoot();



}
