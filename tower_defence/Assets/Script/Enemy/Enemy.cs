using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int wayPointCount;              // 이동 경로 개수
    Transform[] wayPoints;          // 이동 경로 정보
    int currentIndex = 0;           // 현재 목표지점 인덱스
    Movement movement;              // 오브젝트 이동 제어

    public int maxHP = 100;         // 최대 HP
    int hp;                         // 현재 HP

    // 아이템 관련 ----------------------------------------------------------
    public List<BuffBase> onBuff;
    // ---------------------------------------------------------------------

    IEnumerator fireItemDamage;

    public int MaxHP
    {
        get => maxHP;
        set
        {
            maxHP = value;
        }
    }

    public int Hp
    {
        get => hp;
        set
        {
            hp = value;

            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();

        // 아이템 관련 ----------------------------------------------------------
        onBuff = new List<BuffBase>();
        //-----------------------------------------------------------------------
    }

    private void Start()
    {
        hp = maxHP;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            MaxHP -= (int)bullet.Power;

            //Debug.Log($"플레이어의 현재HP는 {Hp}");
        }
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
            hp -= damage;              // damage만큼 hp 감소

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

    //public void BuffOnOff(float value, bool onbuff, int buffIdax)
    //{
    //    switch (buffIdax)
    //    {
    //        case 2:                                       // 이동속도 둔화
    //            if (BuffOverlap(onbuff, buffIdax))
    //            {
    //                // 1.0f ~ 이상 (공격력 증가량)
    //                movement.MoveSpeed = movement.MoveSpeed - (movement.MoveSpeed * value);                // 이동속도 둔화량 만큼 둔화
    //            }
    //            if (buffEA[buffIdax] == 0)
    //            {
    //                movement.MoveSpeed = movement.OriginalMoveSpeed; // 원래 이동속도로 복귀
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //bool BuffOverlap(bool onbuff, int index)        // 버프 중복 적용 확인 
    //{
    //    bool result = false;
    //    if (onbuff)                     // 버프 받은 상태면 (공격력 증가 효과를 중첩으로 받지 않게 하기 위함)
    //    {
    //        if (!isOnBuffPower[index])         // 현재 버프효과를 받고 있지 않으면
    //        {
    //            isOnBuffPower[index] = true;   // 버프 받은 상태
    //            result = true;
    //        }
    //        buffEA[index]++;                   // 중첩된 갯수 증가
    //    }
    //    else                            // 버프 받은 상태 해제면
    //    {
    //        buffEA[index]--;                   // 중첩된 갯수 감소
    //        if (buffEA[index] == 0)
    //        {
    //            isOnBuffPower[index] = false;      // 버프 해제 상태 
    //            buffEA[index] = 0;
    //        }
    //    }
    //    return result;
    //}



    /// <summary>
    /// 버프 on / off하는 함수
    /// </summary>
    /// <param name="Type">버프의 타입</param>
    /// <param name="origin">오리지날 상태 수치</param>
    /// <returns></returns>
    float BuffChange(BuffType Type, float origin)
    {
        if (onBuff.Count > 0)
        {
            bool buffCheck = false;             // 자기 자신을 찾았는지 확인하는 변수
            float temp = 0;
            for (int i = 0; i < onBuff.Count; i++)
            {
                if (onBuff[i].buffType.Equals(Type))
                {
                    buffCheck = true;
                    temp += origin * onBuff[i].percentage;      // 해당 버프의 퍼센트(%) 수치만큼 적용
                    break;
                }
            }

            if (!buffCheck)         // 자기 자신을 찾지 못 했을 겅우
            {
                // 버프 해제
                return origin;      // 원래 상태로 돌아가기
            }
            else
            {
                // 버프 적용
                return temp;        // 자기 자신을 찾았을 경우, 그 효과로 적용
            }
        }
        else
        {
            // 적용 받은 버프가 없으면 원래 상태로
            return origin;
        }
    }

    /// <summary>
    /// 버프를 선택하는 함수
    /// </summary>
    /// <param name="type">버프의 타입</param>
    public void ChooseBuff(BuffType type)
    {
        switch (type)
        {
            case BuffType.Power:
                break;
            case BuffType.AttactSpeed:
                break;
            case BuffType.Slow:
                movement.MoveSpeed = BuffChange(type, movement.OriginalMoveSpeed);
                break;
            case BuffType.Stun:
                // 스턴 효과가 남아있으면 true, 없으면 false로 적용
                // 1.0f을 넣은 이유는 스턴은 무조건 temp 값이 0이기 때문
                movement.IsStun = BuffChange(type, 1.0f) < 0.1f ? true : false;
                break;
            default:
                break;
        }
    }
}
