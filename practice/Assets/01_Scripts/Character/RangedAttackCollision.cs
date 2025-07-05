using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackCollision : AttackCollision
{
    [SerializeField] bool IsPushObject = false;       // �Ҹ� ����

    protected override void GiveDamage(Collider collision)
    {
        base.GiveDamage(collision);
        // �Ҹ�
        if (IsPushObject == true)
            PushObject();
    }
}
