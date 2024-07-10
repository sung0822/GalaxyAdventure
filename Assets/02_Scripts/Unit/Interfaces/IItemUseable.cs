using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemUseable
{ 
    public void UseItem(ItemType itemType);

    public void ChangeSelectedItem(ItemType itemType);

}
