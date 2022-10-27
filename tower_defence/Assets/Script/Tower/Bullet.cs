using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly int HP;
    float BulletDamage = 1000.0f;


    float speed = 10.0f; // 총알 스피드
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed; // 총알에게 velocity값을 준다.(일정하게 움직이기 위해)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float EnemyDamage = (HP - BulletDamage);
            Destroy(collision.gameObject);
            Destroy(this.gameObject); // 적을 만나면 총알을 제거
        }
    }
}
