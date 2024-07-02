using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarryable 
{
    public Inventory inventory { get; set; }
    void GiveItem(ItemBase item);

}
