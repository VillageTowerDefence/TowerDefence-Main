using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Tower_PowerUp_Use : MonoBehaviour
{
    [Header("타워 공격력 증가량")]
    [Range(1.0f, 10.0f)]
    public float powerUPdagage = 1.5f;         // 타워 공격력 증가량
    [Header("버프 지속시간")]
    public float buffTime = 5.0f;
    
    private void Start()
    {
        Destroy(this.gameObject, buffTime+1.0f);
        StartCoroutine(BuffStop());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            var tower = collision.GetComponent<Tower>();
            tower.BuffPowerUp(powerUPdagage, true);         // 공격력 증가량 만큼 증가
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            var tower = collision.GetComponent<Tower>();
            tower.BuffPowerUp(1.0f, false);                 // 원상태 복귀
        }
    }

    IEnumerator BuffStop()
    {
        yield return new WaitForSeconds(buffTime);
        gameObject.SetActive(false);
    }
}
