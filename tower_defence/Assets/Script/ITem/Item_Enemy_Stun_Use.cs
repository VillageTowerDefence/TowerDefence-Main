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
        anim.SetBool("TimerEnd", true);
        yield return new WaitForSeconds(1.0f);
        col.enabled = false;
        anim.SetBool("TimerEnd", false);
        foreach (var enemy in enemies)
        {
            enemy.OnStun(stunTime);
        }

        explosion.gameObject.SetActive(true);
        Destroy(explosion.gameObject, 1.0f);
        Destroy(this.gameObject, 1.0f);
        explosion.parent = null;
        gameObject.SetActive(false);
    }
}
