using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : Enemy
{
    private void Awake()
    {
        Hp = hp * 2;
    }
}
