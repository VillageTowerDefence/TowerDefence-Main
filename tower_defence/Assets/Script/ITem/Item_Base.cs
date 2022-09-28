using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Base : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform root;

    public int itemEA = 0;
    bool itemUse = false;

    private void Start()
    {
        root = transform.root;  // 최상위 오브젝트를 불러온다.
    }



    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)  // 드래그 시작
    {
        if (itemEA != 0)
        {
            itemUse = true;
            root.BroadcastMessage("BeginDarg", transform, SendMessageOptions.DontRequireReceiver);  // 해당함수가 없어도 오류가 나지 않도록 함
        }
        else
        {
            itemUse = false;
            Debug.Log("아이템이 없습니다.");
        }
    }

    public void OnDrag(PointerEventData eventData)      // 드래그 중
    {
        if (itemUse)
        {
            root.BroadcastMessage("Darg", transform, SendMessageOptions.DontRequireReceiver);
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)   // 드래그 끝
    {
        // 아이템 사용
        if (itemUse)
        {
            root.BroadcastMessage("EndDarg", transform, SendMessageOptions.DontRequireReceiver);
            itemUse = false;
            itemEA--;
            if (itemEA < 0)
            {
                itemEA = 0;
            }
        }
    }

}
