using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    Vector3 moveDir = Vector3.zero;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    


    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    public void MoveTo(Vector3 newPosition)
    {
        moveDir = newPosition;      // newPosition으로 방향 설정
    }

}
