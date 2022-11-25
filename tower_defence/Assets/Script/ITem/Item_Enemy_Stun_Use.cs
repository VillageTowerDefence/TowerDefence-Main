using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Enemy_Stun_Use : MonoBehaviour
{
    [Header("기절시킬 적의 수 : 최대n명")]
    public int enemies = 1;
    [Header("스턴 지속 시간")]
    public float stunTime = 5.0f;
    [Header("스턴 범위")]
    public float stunRange = 1.0f;

    private void Awake()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = stunRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Movement enemy = collision.GetComponent<Movement>();
            enemy.OnStun(stunTime);
            Destroy(this.gameObject);
        }
    }
}
