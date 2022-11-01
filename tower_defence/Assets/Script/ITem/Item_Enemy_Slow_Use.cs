using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Enemy_Slow_Use : Item_BuffBase
{
    [Header("버프 지속 시간")]
    public float time = 0.0f;
    [Header("이동속도 감소량")]
    [Range(0.0f, 1.0f)]
    public float speedSlow = 0.0f;
    // const string buffName = "Power";

    void Start()
    {
        buffTime = time;            // 버프 지속시간 설정
        BuffState = BuffType.Slow; // 버프 타입 변경
        StartCoroutine(Ondestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))              // 태그가 적이면
        {
            BuffOn(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))              // 태그가 적이면
        {
            BuffOff(collision);
        }
    }

    void BuffOn(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)               // tower가 널 이아니면
        {
            enemy.BuffOnOff(speedSlow, true, buffIndex);       // 타워 공격속도 버프 함수 실행
        }
    }

    void BuffOff(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)               // tower가 널 이아니면
        {
            enemy.BuffOnOff(speedSlow, false, buffIndex);      // 터워 공격속도 버프 함수 해제
        }
    }

    IEnumerator Ondestroy()
    {
        Destroy(this.gameObject, time + 1);

        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
