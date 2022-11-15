using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifePlus : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        Hp = hp * 2;    // hp 2배
    }
}
