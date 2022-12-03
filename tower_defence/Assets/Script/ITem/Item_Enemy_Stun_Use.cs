using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Enemy_Stun_Use : MonoBehaviour
{
    [Header("스턴 지속 시간")]
    public float stunTime = 5.0f;
    [Header("스턴 범위")]
    public float stunRange = 1.0f;

    List<Movement> enemies;

    Animator anim;
    CircleCollider2D col;
    Transform explosion;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.radius = stunRange;
        anim = GetComponent<Animator>();
        explosion = transform.GetChild(2);
        enemies = new List<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.GetComponent<Movement>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))          //적이 나갈때
        {
            foreach (var enemy in enemies)
            {
                if (enemy == collision.gameObject)  // 나간 적이 리스트에 있다면
                {
                    enemies.Remove(enemy);          //그 적을 리스트에 제거
                    break;
                }
            }
        }
    }

    public IEnumerator StartTimer()
    {
        anim.SetBool("TimerEnd", true);             // 애니메이션 작동
        yield return new WaitForSeconds(1.0f);      // 1초 뒤에
        anim.SetBool("TimerEnd", false);            // 애니메이션 중지
        col.enabled = false;                        // 콜라이더 끄기
        foreach (var enemy in enemies)
        {
            enemy.OnStun(stunTime);                 // 적한테 스턴 적용
        }

        explosion.gameObject.SetActive(true);       // 폭발 오브젝트 켜기
        Destroy(explosion.gameObject, 1.0f);        // 폭발 오브젝트 삭제
        Destroy(this.gameObject, 1.0f);             // 자신 삭제
        explosion.parent = null;                    // 자식 해제
        gameObject.SetActive(false);                // 오브젝트 끄기
    }
}
