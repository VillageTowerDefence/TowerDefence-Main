using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeMinus : Enemy
{
    private void Awake()
    {
        Hp = hp / 2;
    }
}
