using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Item_Base : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("아이템 프리펩")]
    public GameObject prePrefab;        // 아이템 프리펩
    [Header("설치 가능한 레이어 이름 / 아무것도 안쓰면 모두 설치가능")]
    public string layerName;            // 레이어 이름
    [Space(10f)]
    //[SerializeField]    // private이지만 인스팩터창에서만 public 처럼 사용할 수 있다. (본질은 private임)
    [Header("아이템 수량")]
    public int itemEA = 0;                     // 아이템 총 갯수

    Transform root;                     // 최상단 부모
    Transform parent;                   // 자기 부모를 찾음
    TextMeshProUGUI itemText;           // 아이템 수량 표시용
    new Camera camera;                  // 마우스 좌표을 신 좌표 값으로 변환하기 용
    UnityEngine.UI.Image image;         // 아이템 이미지 알파값 바꾸기용

    Vector3 MousDir;                    // 아이템 최종 생성 좌표
    RaycastHit hit;                     // 레이캐스트 용 레이저
    int layerMask;                      // 레이캐스트 검출할 레이어 (layerName를 검출)
    float distance = 11.0f;             // 레이캐스트 거리  

    bool itemUse = false;               // 아이템을 사용할 수 있는지 표시(flase면 불가능, true면 가능)

    int ItemEA                          // itemEA 프로퍼티
    {
        get => itemEA;

        set
        {
            itemEA = value;

            if (itemEA < 0)         // 0보다 작으면
            {
                itemEA = 0;         // 갯수 0으로 고정
            }

            itemText.text = $"{itemEA}";        // itemEA수량의 맞게 UI의 표시

            if (itemEA <= 0)        // 아이템 갯수가 0보다 작거나 같으면 alpha값 변경 (아이템이 0개면 회색으로 보이게 하기 위한 기능)
            {
                image.color = Color.clear;          // 0이면 투명
            }
            else
            {
                image.color = Color.white;          // 1이면 정상으로 바꾸기
            }
        }
    }

    protected virtual void Awake()
    {
        image = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        itemText = transform.parent.GetComponentInChildren<TextMeshProUGUI>();      // 부모에 있는 자식들 중에 TextMeshProUGUI를 찾아라
    }

    protected virtual void Start()
    {
        root = transform.root;  // 최상위 오브젝트를 불러온다.
        parent = transform.parent;      // parent의 부모 트랜스폼을 넣는다.
        transform.position = parent.position;   // 자신의 위치를 parent의 위치로 가게한다.
        itemText.text = $"{itemEA}";        // 현재 아이템 소유량
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();      // 하이라키창에 있는 게임오브젝트 중에 "MainCamera"이름이 들어간 오브젝트 찾기
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)  // 드래그 시작
    {
        if (ItemEA != 0)        // 아이템 갯수가 0이 아니면
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
        if (itemUse)        // 아이템을 사용할 수 있으면
        {
            root.BroadcastMessage("Darg", transform, SendMessageOptions.DontRequireReceiver);
            transform.position = eventData.position;    // 마우스를 따라가게 함
        }
    }

    public void OnEndDrag(PointerEventData eventData)   // 드래그 끝
    {
        // 아이템 사용
        if (itemUse)        // 아이템을 사용할 수 있으면
        {
            layerMask = 1 << LayerMask.NameToLayer(layerName);               // 해당 레이어를 layerName로 설정
            MousDir = camera.ScreenToWorldPoint(eventData.position);         // 스크린 좌표를 월드좌표로 변환
            Vector3 laydir = MousDir;
            MousDir.z = 0.0f;           // 카메라 기준의 값이기때문에 -10이 들어가 있다(-10이 있으면 게임 카메라에 안그려진다.)

            if(layerName == "")         // 검출할 레이어가 필요없으면 (모든 구역 설치 가능)
            {
                if (Physics.Raycast(laydir, Vector3.forward, out hit, distance))                // 그 해당 좌표에 동작 (레이어 검출x)
                {
                    ItemSawpn();
                }
                else
                {
                    Debug.Log("그 위치에는 사용할 수 없습니다.");
                }
            }
            else                        // 검출할 레이어가 있다면 (해당 구역만 설치 가능)
            {
                if (Physics.Raycast(laydir, Vector3.forward, out hit, distance, layerMask))       // 그 해당 좌표에 해당 레이어가 있으면 동작
                {
                    ItemSawpn();
                }
                else
                {
                    Debug.Log("그 위치에는 사용할 수 없습니다.");
                }
            }

            itemUse = false;        // 아이템을 사용할 수 없다.
            transform.position = parent.position;   // 원래 자리로 돌아가기
        }
    }

    void ItemSawpn()
    {
        Tile_Obstacle tile = hit.transform.GetComponent<Tile_Obstacle>();
        if(tile != null)
        {
            if (tile.IsBuildItem == true)
            {
                Debug.Log("이미 설치 되어있습니다.");
                return;
            }
            tile.IsBuildItem = true;
        }
        root.BroadcastMessage("EndDarg", transform, SendMessageOptions.DontRequireReceiver);
        ItemEA--;           // 아이템 갯수 1 감소

        Instantiate(prePrefab, hit.transform.position, Quaternion.identity);       // 아이템 프리펩 생성 (MousDir 위치에 생성한다.) MousDir : 최종 좌표

        ItemUse();          // 그 아이템 효과 사용
    }

    /// <summary>
    /// 아이템 사용 함수
    /// </summary>
    protected virtual void ItemUse()
    {
       
    }
}
