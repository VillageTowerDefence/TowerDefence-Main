using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Buff : MonoBehaviour
{
    private float buffTime = 0f;
    string Name;

    /// <summary>
    /// 버프 관리
    /// </summary>
    /// <param name="buffName">버프 이름</param>
    protected void buff(string buffName)
    {
        switch (buffName)
        {
            case "Power":
                Name = buffName;
                break;
            case "다음버프":

                break;
            default:
                break;
        }

        return;
    }

    /// <summary>
    /// 버프 상태 적용
    /// </summary>
    /// <param name="thisObj">버프의 오브젝트 (코루틴에 쓰임)</param>
    /// <param name="collision">버프 효과를 받을 타워 or 적</param>
    /// <param name="time">버프 지속 시간</param>
    /// <param name="value"></param>
    protected void buffOn(GameObject thisObj, Collider2D collision, float time, float value)
    {
        switch (Name)
        {
            case "Power":
                var tower = collision.GetComponent<Tower>();
                tower.BuffPowerUp(value, true);         // 공격력 증가량 만큼 증가
                buffTime = time;
                StartCoroutine(DestroyTimer(thisObj));
                StartCoroutine(Rotate(thisObj));
                break;
            case "다음버프":

                break;
            default:
                break;
        }
        return;
    }

    /// <summary>
    /// 버프 상태 해제
    /// </summary>
    /// <param name="collision">버프 해제할 대상</param>
    protected void buffOff(Collider2D collision)
    {
        switch (Name)
        {
            case "Power":
                var tower = collision.GetComponent<Tower>();
                tower.BuffPowerUp(1.0f, false);

                break;
            case "다음버프":

                break;
            default:
                break;
        }

        return;
    }

    IEnumerator DestroyTimer(GameObject obj)
    {
        Destroy(obj, buffTime + 1.0f);
        yield return new WaitForSeconds(buffTime);          // 버프 끝나는 시간

        obj.SetActive(false);
    }

    IEnumerator Rotate(GameObject obj)
    {
        while (true)
        {
            obj.transform.Rotate(-Vector3.forward * 0.15f);  // 

            yield return null;
        }
    }
}
