using UnityEngine;

public interface IWeapon
{
    Unit user { get; set; }

    float shootCycle { get; set; }
    public void Use();
}