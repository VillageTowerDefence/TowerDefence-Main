using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Item_Base : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform root;         // 최상단 부모

    Transform parent;       // 자기 부모를 찾음

    UnityEngine.UI.Image image;

    public GameObject prePrefab;        // 아이템 프리펩

    TextMeshProUGUI itemText;

    new Camera camera;

    public string layerName;          // 레이어 이름

    int layerMask;

    RaycastHit hit;

    float distance = 11.0f;         // 레이캐스트 거리  

    Vector3 MousDir;        // 최종 생성 좌표


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
                itemEA = 0;     // 갯수 0으로 고정
            }

            itemText.text = $"{itemEA}";

            if (itemEA <= 0)     // 아이템 갯수가 0보다 작거나 같으면 alpha값 변경
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

    protected virtual void Awake()
    {
        image = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        itemText = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void Start()
    {
        root = transform.root;  // 최상위 오브젝트를 불러온다.
        parent = transform.parent;      // parent의 부모 트랜스폼을 넣는다.
        transform.position = parent.position;   // 자신의 위치를 parent의 위치로 가게한다.
        itemText.text = $"{itemEA}";        // 현재 아이템 소유량
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }



    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)  // 드래그 시작
    {
        if (ItemEA != 0)
        {
            itemUse = true;     // 아이템을 사용할 수 있다.
            root.BroadcastMessage("BeginDarg", transform, SendMessageOptions.DontRequireReceiver);  // 해당함수가 없어도 오류가 나지 않도록 함
        }
        else
        {
            itemUse = false;    // 아이템을 사용할 수 없다.
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
        if (itemUse)        // 아이템을 사용할 수 있으면
        {
            layerMask = 1 << LayerMask.NameToLayer(layerName);
            MousDir = camera.ScreenToWorldPoint(eventData.position);         // 스크린 좌표를 월드좌표로 변환
            Vector3 laydir = MousDir;
            MousDir.z = 0.0f;           // 카메라 기준의 값이기때문에 -10이 들어가 있다(-10이 있으면 게임 카메라에 안그려진다.)

            if (Physics.Raycast(laydir, Vector3.forward, out hit, distance, layerMask))       // 그 해당 좌표에 해당 태그가 있으면 동작
            {
                root.BroadcastMessage("EndDarg", transform, SendMessageOptions.DontRequireReceiver);
                itemUse = false;        // 아이템을 사용할 수 없다.
                ItemEA--;           // 아이템 갯수 1 감소

                Instantiate(prePrefab, MousDir, Quaternion.identity);       // 아이템 프리펩 생성

                ItemUse();  // 그 아이템 효과 사용 (게임스크린 좌표값(마우스)을 보냄)
            }
            else
            {
                itemUse = false;        // 아이템을 사용할 수 없다.
                Debug.Log("그 위치에는 사용할 수 없습니다.");
            }

            transform.position = parent.position;   // 원래 자리로 돌아가기
        }

    }

    protected virtual void ItemUse()
    {
        
    }

}
