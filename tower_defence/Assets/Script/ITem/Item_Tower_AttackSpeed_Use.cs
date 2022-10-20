using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Tower_AttackSpeed_Use : Item_Buff
{
    [Header("버프 지속 시간")]
    public float time = 0.0f;
    [Header("타워 공격속도 증가량")]
    [Range(0.0f, 1.0f)]
    public float attackSpeed = 0.0f;

    const string buffName = "AttackSpeed";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            buffOn(this.gameObject, collision, buffName, time, attackSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            buffOff(collision, buffName);
        }
    }
}
