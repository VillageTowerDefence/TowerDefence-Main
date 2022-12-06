using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBase : MonoBehaviour
{
    public BuffType buffType;       // 버프 타입
    public float percentage;        // 변화량
    public float duration;          // 지속시간
    public float currentTime;

    Tower tower;
    Enemy enemy;

    WaitForSeconds seconds = new WaitForSeconds(0.1f);

    /// <summary>
    /// 초기화 작업
    /// </summary>
    public void Initialize(BuffType type, float value, float time, Tower tower)
    {
        buffType = type;
        percentage = value;
        duration = time;
        currentTime = duration;
        if(tower != null)
        {
            this.tower = tower;
            tower.onBuff.Add(this);
            tower.ChooseBuff(type);
        }
        Execute();
    }

    public void Initialize(BuffType type, float value, float time, Enemy enemy)
    {
        buffType = type;
        percentage = value;
        duration = time;
        currentTime = duration;
        if (enemy != null)
        {
            this.enemy = enemy;
            //enemy.onBuff.Add(this);
            //enemy.ChooseBuff(type);
        }
        Execute();
    }

    /// <summary>
    /// 버프의 효과를 적용하고 타이머를 적용하는 함수
    /// </summary>
    protected virtual void Execute()
    {
        // 타워 리스트 추가
        // 타워에 ChooseBuff 함수 호출
        StartCoroutine(Activation());
    }

    /// <summary>
    /// 지속시간 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Activation()
    {
        while (currentTime > 0)
        {
            currentTime -= 0.1f;
            yield return seconds;
        }
        currentTime = 0;
        Deactivaltion();
    }

    /// <summary>
    /// 타이머가 끝나면 버프 제거
    /// </summary>
    void Deactivaltion()
    {
        if (tower != null)
        {
            tower.onBuff.Remove(this);
            tower.ChooseBuff(buffType);
        }
        else
        {

        }
        // 타워 리스트 지우고
        // 함수 다시한번 호출
        Destroy(gameObject);
    }
}
