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
    // 아이템 관련 ----------------------------------------------------------
    bool[] isOnBuffPower;      // 현재 파워아템 효과를 받고 있는지 (중첩 x)
    int[] buffEA;                  // 현재 버프가 몇개 겹쳤는지 확인
    string[] buffKind = { "Power", "AttackSpeed", "Slow"};        // 버프 종류
    // ---------------------------------------------------------------------

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

        // 아이템 관련 ----------------------------------------------------------
        isOnBuffPower = new bool[buffKind.Length];
        for (int i = 0; i < buffKind.Length; i++)
        {
            isOnBuffPower[i] = false;
        }

        buffEA = new int[buffKind.Length];
        for (int i = 0; i < buffKind.Length; i++)
        {
            buffEA[i] = 0;
        }
        //-----------------------------------------------------------------------
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

    public void BuffOnOff(float value, bool onbuff, int buffIdax)
    {
        switch (buffIdax)
        {
            case 2:                                       // 파워 업
                if (BuffOverlap(onbuff, buffIdax))
                {
                    // 1.0f ~ 이상 (공격력 증가량)
                    movement.MoveSpeed = movement.MoveSpeed - (movement.MoveSpeed * value);                // 공격력 증가량 만큼 증가
                }
                if (buffEA[buffIdax] == 0)
                {
                    movement.MoveSpeed = movement.OriginalMoveSpeed; // 원래 공격력으로 복귀
                }
                break;
            //case 1:                                      // 공격속도 업
            //    if (BuffOverlap(onbuff, buffIdax))
            //    {
            //        // 0.0f ~ 1.0f (공격속도 증가량)
            //        attackSpeed = attackSpeed - (attackSpeed * value);      // 일정 수치 만큼 공격속도 증가
            //    }
            //    if (buffEA[buffIdax] == 0)
            //    {
            //        attackSpeed = originalAttackSpeed;   // 원래 공격속도로 복귀
            //    }
                //break;
            default:
                break;
        }
    }

    bool BuffOverlap(bool onbuff, int index)        // 버프 중복 적용 확인 
    {
        bool result = false;
        if (onbuff)                     // 버프 받은 상태면 (공격력 증가 효과를 중첩으로 받지 않게 하기 위함)
        {
            if (!isOnBuffPower[index])         // 현재 버프효과를 받고 있지 않으면
            {
                isOnBuffPower[index] = true;   // 버프 받은 상태
                result = true;
            }
            buffEA[index]++;                   // 중첩된 갯수 증가
        }
        else                            // 버프 받은 상태 해제면
        {
            buffEA[index]--;                   // 중첩된 갯수 감소
            if (buffEA[index] == 0)
            {
                isOnBuffPower[index] = false;      // 버프 해제 상태 
                buffEA[index] = 0;
            }
        }
        return result;
    }
}
