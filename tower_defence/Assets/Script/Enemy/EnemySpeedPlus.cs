using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedPlus : Movement
{
    private void Awake()
    {
        MoveSpeed = moveSpeed * 2;  // 이동속도 2배
    }
}
