using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float power = 0.0f;
    float speed = 10.0f; // 총알 스피드
    bool isphysics; // true 물리 false 마법
    Rigidbody2D rb;

    private void Start()
    {
        Debug.Log(Power);
    }

    public float Power
    {
        get => power;
        set
        {
            power = value;
        }
    }
    
    public bool IsPhysics
    {
        get => isphysics;
        set
        {
            isphysics = value;
        }
    }



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
            Destroy(this.gameObject); // 적을 만나면 총알을 제거
        }
    }
}
