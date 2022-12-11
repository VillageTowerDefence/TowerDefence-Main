using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    float originalMoveSpeed = 0.0f;

    Vector3 moveDir = Vector3.zero;

    bool isStun = false;

    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
        }
    }

    /// <summary>
    /// 스턴 확인상태 프러퍼티
    /// </summary>
    public bool IsStun
    {
        set
        {
            isStun = value;
        }
    }

    public float OriginalMoveSpeed => originalMoveSpeed;

    private void Start()
    {
        originalMoveSpeed = MoveSpeed;
    }

    private void Update()
    {
        if (!isStun)            // 스턴 상태가 아니면
        {
            transform.position += moveSpeed * Time.deltaTime * moveDir; 
        }
    }

    public void MoveTo(Vector3 newPosition)
    {
        moveDir = newPosition;      // newPosition으로 방향 설정
    }
}
