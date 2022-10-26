using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_BuffBase : MonoBehaviour
{
    protected float buffTime = 0f;        // 버프 지속 시간
    WaitForSeconds buffRotate;          // 회전하는 시간 (시간이 길수록 이상하게 돌음)
    protected int buffIndex = 0;                  // 해당 버프 인덱스

    protected enum BuffType             // 버프 타입
    {
        Power,          // 공격력 증가
        Speed           // 공격속도 증가
    }

    BuffType buffState = BuffType.Power;    // 버프 상태
    protected BuffType BuffState            // 버프 상태 프로퍼티
    {
        get => buffState;
        set
        {
            buffState = value;

            switch (buffState)
            {
                case BuffType.Power:
                    buffIndex = 0;
                    break;
                case BuffType.Speed:
                    buffIndex = 1;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 버프 시작 함수
    /// </summary>
    /// <param name="obj">이펙트 오브젝트</param>
    protected void BuffStart(GameObject obj)
    {
        buffRotate = new WaitForSeconds(0.02f);         // 회전 속도 설정
        StartCoroutine(DestroyTimer(obj));              // 이펙트 오브젝트 삭제 코루틴
        StartCoroutine(Rotate(obj));                    // 이펙트 오브젝트 회전 코루틴
    }

    /// <summary>
    /// 오브젝트 삭제
    /// </summary>
    /// <param name="obj">삭제 시킬 이펙트 오브젝트</param>
    /// <returns></returns>
    IEnumerator DestroyTimer(GameObject obj)
    {
        Destroy(obj, buffTime + 1.0f);                      // 오브젝트 삭제 (버프 시간보다 1초 길게한 이유 : 오브젝트가 먼저 없어지면 컬리전Exit가 안됨)
        yield return new WaitForSeconds(buffTime);          // 버프 끝나는 시간

        obj.SetActive(false);                               // 오브젝트 끄기
    }

    /// <summary>
    /// 오브젝트 회전
    /// </summary>
    /// <param name="obj">회전 시킬 이펙트 오브젝트</param>
    /// <returns></returns>
    IEnumerator Rotate(GameObject obj)
    {
        while (true)
        {
            obj.transform.Rotate(-Vector3.forward);  // 시계방향으로 돌림

            yield return buffRotate;
        }
    }
}
