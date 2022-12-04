using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Magic_Up : Enemy
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();// 불렛찾고
            if (!bullet.IsPhysics)   // 물리공격이 아니면(마법공격이면)
            {
                Hp -= (int)bullet.Power / 2;    // 절반데미지 받기
            }
            else                    // 물리 공격이면
            {
                Hp -= (int)bullet.Power;        // 통상 데미지
            }
            //Debug.Log($"플레이어의 HP는 {Hp}");
        }
    }
}
