using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Enemy_Stun_Use : MonoBehaviour
{
    [Header("기절시킬 적의 수 : 최대n명")]
    public int enemies = 1;
    [Header("스턴 지속 시간")]
    public float stunTime = 5.0f;
    List<Movement> enemyList;
    int count = 0;
    public Action onStun;
    bool stunUse = false;

    private void Awake()
    {
        enemyList = new List<Movement>();
    }

    private void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (count < enemies)
        {
            if (collision.CompareTag("Enemy"))
            {
                enemyList.Add(collision.GetComponent<Movement>());
                count++;
            }
        }
        else
        {
            if (!stunUse)
            {
                stunUse = true;
                StartCoroutine(EnemyStun());
            }
        }
    }

    IEnumerator EnemyStun()
    {
        float enemyoriginalMoveSpeed = 0.0f;
        foreach (var enemy in enemyList)
        {
            enemyoriginalMoveSpeed = enemy.moveSpeed;
            enemy.moveSpeed = 0.0f;
        }

        yield return new WaitForSeconds(stunTime);
        foreach (var enemy in enemyList)
        {
            enemy.moveSpeed = enemyoriginalMoveSpeed;
        }
    }
}
