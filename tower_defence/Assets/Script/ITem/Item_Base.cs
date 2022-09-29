using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Base : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform root;         // 최상단 부모

    Transform parent;       // 자기 부모를 찾음

    Image image;

    public int itemEA = 0;
    int alpha = 0;

    int ItemEA      // itemEA 프로퍼티
    {
        get => itemEA;

        set
        {
            itemEA = value;

            if (itemEA < 0)
            {
                itemEA = 0;
            }

            if(itemEA <= 0)     // 아이템 갯수가 0보다 작거나 같으면 alpha값 변경
            {
                alpha = 0;      // 0이면 투명
            }
            else
            {
                alpha = 1;      // 1이면 정상으로 바꾸기
            }

            image.color = new Color(1, 1, 1, alpha);    // image의 컬러를 변경(이 경우에는 알파값만 변경)
        }
    }
    bool itemUse = false;

    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        root = transform.root;  // 최상위 오브젝트를 불러온다.
        parent = transform.parent;      // parent의 부모 트랜스폼을 넣는다.
        transform.position = parent.position;   // 자신의 위치를 parent의 위치로 가게한다.
    }



    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)  // 드래그 시작
    {
        if (ItemEA != 0)
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
            ItemEA--;           // 아이템 갯수 1 감소
            
            transform.position = parent.position;   // 원래 자리로 돌아가기

            ItemUse();  // 그 아이템 효과 사용
        }

    }

    protected virtual void ItemUse()
    {
        // Debug.Log("아이템을 사용했습니다.");
    }

}
