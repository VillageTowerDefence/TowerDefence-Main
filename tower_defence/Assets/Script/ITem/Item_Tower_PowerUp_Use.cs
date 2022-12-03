using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Tower_PowerUp_Use : Item_BuffBase
{
    [Header("버프 지속 시간")]
    public float time = 0.0f;
    [Header("타워 공격력 증가량")]
    [Range(1.0f, 3.0f)]
    public float damage = 0.0f;
    
    List<Tower> towers;

    private void Awake()
    {
        towers = new List<Tower>();
    }

    void Start()
    {
        buffTime = time;            // 버프 지속시간 설정
        BuffState = BuffType.Power; // 버프 타입 변경
        BuffStart(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))              // 태그가 타워면
        {
            BuffOn();
            towers.Add(collision.GetComponent<Tower>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))              // 태그가 타워면
        {

            foreach (var tower in towers)
            {
                if(tower == collision.GetComponent<Tower>())
                {
                    BuffOff(tower);
                    towers.Remove(tower);
                    break;
                }
            }
        }
    }

    void BuffOn()
    {
        foreach (var tower in towers)
        {
            tower.BuffOnOff(damage, true, buffIndex);       // 타워 공격속도 버프 함수 실행
        }
    }

    void BuffOff(Tower tower)
    {
        if (tower != null)               // tower가 널 이아니면
        {
            tower.BuffOnOff(damage, false, buffIndex);      // 터워 공격속도 버프 함수 해제
        }
    }
}
