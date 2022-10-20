using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Tower_PowerUp_Use : Item_Buff
{
    [Header("버프 지속 시간")]
    public float time = 0.0f;
    [Header("타워 공격력 증가량")]
    [Range(1.0f, 3.0f)]
    public float damage = 0.0f;
    const string buffName = "Power";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            buffOn(this.gameObject, collision, buffName, time, damage);
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
