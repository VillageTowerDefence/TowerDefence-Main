using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeed : Movement
{
    private void Awake()
    {
        MoveSpeed = moveSpeed * 2;
    }
}
