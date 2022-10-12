using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Item_Bomb_Use : MonoBehaviour
{
    public Action onItemUse;        // 델리게이트


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleTile"))   // 장애물과 만나면
        {
            StartCoroutine(Distroy());          // 코루틴 시작
        }
    }

    IEnumerator Distroy()
    {
        yield return new WaitForSeconds(2.0f);      // 2초 후에 (라운드 시스템 들어가면 라운드마다 터지게 바꾸기)

        onItemUse?.Invoke();                // Tile_Obstacle로 델리게이트 보냄
        Destroy(this.gameObject);           // 자기 자신 삭제하기
    }
}
