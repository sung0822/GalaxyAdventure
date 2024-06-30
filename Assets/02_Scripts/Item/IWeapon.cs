using UnityEngine;

public interface IWeapon
{
    Unit user { get; set; }
    
    public int power { get; set; }
    float shootCycle { get; set; }
    public void Use();
}