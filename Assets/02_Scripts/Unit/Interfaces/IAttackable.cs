using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    void Attack();
    void StopAttack();
    int power { get; set; }
    bool isAttacking { get; set; }
}
