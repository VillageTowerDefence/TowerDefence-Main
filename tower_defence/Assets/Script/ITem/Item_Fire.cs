using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Item_Fire : Item_Base
{
    public GameObject prePrefab;        // 아이템 프리펩

    new Camera camera;

    Vector3 MousDir;        // 최종 생성 좌표


    protected override void Start()
    {
        base.Start();
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    protected override void ItemUse(PointerEventData eventData)
    {
        MousDir = ScreenPotion(eventData.position);     // ScreenPotion 함수 사용(매개변수 : 스크린좌표)
        MousDir.z = 0.0f;           // 카메라 기준의 값이기때문에 -10이 들어가 있다(-10이 있으면 게임 카메라에 안그려진다.)
        Debug.Log(MousDir);

        Instantiate(prePrefab, MousDir, Quaternion.identity);       // 아이템 프리펩 생성
    }

    Vector3 ScreenPotion(Vector2 position)
    {
        return camera.ScreenToWorldPoint(position);         // 스크린 좌표를 월드좌표로 변환
    }

}
