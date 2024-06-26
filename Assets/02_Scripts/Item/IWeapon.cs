using UnityEngine;

public interface IWeapon
{
    Unit user { get; set; }
    public void Use();
}