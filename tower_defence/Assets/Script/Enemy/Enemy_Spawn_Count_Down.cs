using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn_Count_Down : MonoBehaviour
{
    public GameObject gameobjForEnemyCount;

    private void Awake()
    {
        gameobjForEnemyCount.GetComponent<EnemySpawner>().enemySpawnCount -= 5;
    }
}
