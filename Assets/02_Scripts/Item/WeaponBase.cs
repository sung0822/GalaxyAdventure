using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public Unit user { get; set; }

    public int power { get; set; }
    public abstract float shootCycle { get; set; }

    public abstract void Use();
}