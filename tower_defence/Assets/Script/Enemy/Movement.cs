using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    float originalMoveSpeed = 0.0f;

    Vector3 moveDir = Vector3.zero;

    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
        }
    }

    public float OriginalMoveSpeed => originalMoveSpeed;

    private void Start()
    {
        originalMoveSpeed = MoveSpeed;
    }

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    public void MoveTo(Vector3 newPosition)
    {
        moveDir = newPosition;      // newPosition으로 방향 설정
    }

    /// <summary>
    /// 적 스턴
    /// </summary>
    /// <param name="time">스턴 시간</param>
    public void OnStun(float time)
    {
        IEnumerator stun = StunTimer(time);
        StopCoroutine(stun);
        moveSpeed = 0.0f;
        StartCoroutine(stun);
    }

    /// <summary>
    /// 스턴 타이머 코루틴
    /// </summary>
    /// <param name="time">스턴 시간</param>
    /// <returns></returns>
    IEnumerator StunTimer(float time)
    {
        yield return new WaitForSeconds(time);
        moveSpeed = originalMoveSpeed;
    }
}
