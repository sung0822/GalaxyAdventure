using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGunState : IShootable
{
    GunItemData gunItemData { get; set; }

}
