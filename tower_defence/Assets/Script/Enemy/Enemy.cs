using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int wayPointCount;              // 이동 경로 개수
    Transform[] wayPoints;          // 이동 경로 정보
    int currentIndex = 0;           // 현재 목표지점 인덱스
    Movement movement;              // 오브젝트 이동 제어

    public int hp = 100;

    IEnumerator fireItemDamage;

    int Hp
    {
        get => hp;
        set
        {
            hp = value;

            if( hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }


    public void Setup(Transform[] wayPoints)
    {
        // 적 이동 경로 wayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;      // 적의 위치를 첫번째 wayPoints 위치로 설정(즉 시작 지점으로 이동)

        StartCoroutine(OnMove());
    }

    IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            transform.Rotate(Vector3.forward * 10);     // 적 오브젝트 회전

            // 적의 현재위치와 목표위치의 거리가 0.02 * movement.moveSpeed보다 작을 때 if 조건문 실행
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement.moveSpeed)    // Vector3.Distance (가장 가까운 오브젝트 찾기)
            {
                NextMoveTo();   // 다음 이동 방향 설정
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if(currentIndex < wayPointCount - 1)        // 아직 이동할 wayPointCount가 남아 있다면
        {
            transform.position = wayPoints[currentIndex].position;  // 다음 이동 경로로 바꿈
            currentIndex++;

            Vector3 dir = (wayPoints[currentIndex].position - transform.position).normalized;   // 다음 목적지의 방향(dir)를 구함
            movement.MoveTo(dir);
        }
        else
        {
            Die();   // 다음 목적지가 없다면 삭제
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);           // 죽으면 오브젝트 삭제
        StopAllCoroutines();                // 모든 코루틴 끄기
    }

    /// <summary>
    /// Fire아이템의 사용 함수
    /// </summary>
    /// <param name="damage">초당 데미지</param>
    public void FireUse(int damage)
    {
        fireItemDamage = FireDamage(damage);
        StartCoroutine(fireItemDamage);
        StartCoroutine(FireItemStop());
    }

    /// <summary>
    /// FireItem으로 몬스터에게 초당 데미지를 주는 코루틴
    /// </summary>
    /// <param name="damage">초당 데미지</param>
    /// <returns></returns>
    IEnumerator FireDamage(int damage)
    {
        while (true)
        {
            Hp -= damage;              // damage만큼 hp 감소

            yield return new WaitForSeconds(1.0f);          // 1초마다 damage의 데미지를 줌 (나중에 변수로 만들어야함)
        }
    }

    /// <summary>
    /// n초 후에 FireItem 효과를 끄는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FireItemStop()
    {
        yield return new WaitForSeconds(5.0f);
        StopCoroutine(fireItemDamage);                      // 5초후에 fireItemDamage 코루틴 끄기 (fireItem 효과 삭제)
    }
}
