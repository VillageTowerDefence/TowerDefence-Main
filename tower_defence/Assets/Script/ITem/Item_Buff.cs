using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Buff : MonoBehaviour
{
    private float buffTime = 0f;

    /// <summary>
    /// 버프 상태 적용
    /// </summary>
    /// <param name="thisObj">버프의 오브젝트 (코루틴에 쓰임)</param>
    /// <param name="collision">버프 효과를 받을 타워 or 적</param>
    /// <param name="time">버프 지속 시간</param>
    /// <param name="value">효과 계수</param>
    protected void buffOn(GameObject thisObj, Collider2D collision, string buffName, float time, float value)
    {
        var tower = collision.GetComponent<Tower>();
        var enemy = collision.GetComponent<Enemy>();
        if(tower != null)
        {
            switch (buffName)
            {
                case "Power":
                    tower.BuffOn(value, true, buffName);         // 공격력 증가량 만큼 증가
                    buffTime = time;
                    StartCoroutine(DestroyTimer(thisObj));
                    StartCoroutine(Rotate(thisObj));
                    break;
                case "AttackSpeed":
                    tower.BuffOn(value, true, buffName);         // 공격력 증가량 만큼 증가
                    buffTime = time;
                    StartCoroutine(DestroyTimer(thisObj));       // 
                    StartCoroutine(Rotate(thisObj));
                    break;
                default:
                    break;
            }
        }
        if(enemy != null)
        {
            switch (buffName)
            {
                case "적 디버프":
                    break;
                default:
                    break;
            }
        }

        return;
    }

    /// <summary>
    /// 버프 상태 해제
    /// </summary>
    /// <param name="collision">버프 해제할 대상</param>
    /// <param name="buffName">버프 이름</param>
    protected void buffOff(Collider2D collision, string buffName)
    {
        var tower = collision.GetComponent<Tower>();
        var enemy = collision.GetComponent<Enemy>();
        if (tower != null)
        {
            switch (buffName)
            {
                case "Power":
                    tower.BuffOn(1.0f, false, buffName);
                    break;
                case "AttackSpeed":
                    tower.BuffOn(1.0f, false, buffName);
                    break;
                default:
                    break;
            }
        }

        if(enemy != null)
        {
            switch (buffName)
            {
                case "적 디버프":
                    break;
                default:
                    break;
            }
        }
        return;
    }

    /// <summary>
    /// 오브젝트 삭제
    /// </summary>
    /// <param name="obj">삭제 시킬 오브젝트</param>
    /// <returns></returns>
    IEnumerator DestroyTimer(GameObject obj)
    {
        Destroy(obj, buffTime + 1.0f);
        yield return new WaitForSeconds(buffTime);          // 버프 끝나는 시간

        obj.SetActive(false);
    }

    /// <summary>
    /// 오브젝트 회전
    /// </summary>
    /// <param name="obj">회전 시킬 오브젝트</param>
    /// <returns></returns>
    IEnumerator Rotate(GameObject obj)
    {
        while (true)
        {
            obj.transform.Rotate(-Vector3.forward * 0.15f);  // 

            yield return null;
        }
    }
}
