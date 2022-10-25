using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Tower_AttackSpeed_Use : Item_BuffBase
{
    [Header("버프 지속 시간")]
    public float time = 0.0f;
    [Header("타워 공격속도 증가량")]
    [Range(0.0f, 1.0f)]
    public float attackSpeed = 0.0f;

    void Start()
    {
        buffTime = time;            // 버프 지속시간 설정
        BuffState = BuffType.Speed; // 버프 타입 변경
        BuffStart(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            BuffOn(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            BuffOff(collision);
        }
    }

    void BuffOn(Collider2D collision)
    {
        Tower tower = collision.GetComponent<Tower>();
        if (tower != null)
        {
            tower.BuffOnOff(attackSpeed, true, buffIndex);
        }
    }

    void BuffOff(Collider2D collision)
    {
        Tower tower = collision.GetComponent<Tower>();
        if (tower != null)
        {
            tower.BuffOnOff(attackSpeed, false, buffIndex);
        }
    }
}
