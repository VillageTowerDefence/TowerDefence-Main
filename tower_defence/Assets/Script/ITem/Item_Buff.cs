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
    /// 버프 사용
    /// </summary>
    /// <param name="collision">버프를 받는 대상</param>
    /// <param name="time">버프 지속시간</param>
    /// <param name="value">버프 효과의 값</param>
    protected void buffOn(Collider2D collision, float time, float value)
    {
        switch (Name)
        {
            case "Power":
                var tower = collision.GetComponent<Tower>();
                tower.BuffPowerUp(value, true);         // 공격력 증가량 만큼 증가
                StartCoroutine(DestroyTimer(collision));
                buffTime = time;
                break;
            case "다음버프":

                break;
            default:
                break;
        }
        return;
    }

    /// <summary>
    /// 버프 해제
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

    IEnumerator DestroyTimer(Collider2D collision)
    {
        Destroy(collision.gameObject, buffTime + 1.0f);
        yield return new WaitForSeconds(buffTime);          // 버프 끝나는 시간

        collision.gameObject.SetActive(false);
    }
}
