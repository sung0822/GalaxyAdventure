using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
    void AttachWeapon(GameObject weapon);
    void SetAimDirection(Vector3 direction);

}
