using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Fire : MonoBehaviour
{
    Transform fire;
    SpriteRenderer sprite;

    float alpha = 1;
    float itemDestroyTime = 5.0f;
    bool onePlay = true;
    bool spriteAlpha = false;

    private void Awake()
    {
        fire = transform.GetChild(0);
        sprite = fire.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        onePlay = true;
        spriteAlpha = false;
        fire.gameObject.SetActive(false);       // 오브젝트 비활성화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (onePlay)
            {
                Debug.Log("트리거");
                fire.gameObject.SetActive(true);        // 오브젝트 활성화
                onePlay = false;
                StartCoroutine(SetAlphaDlray());        // 코루틴 시작(알파 값 딜레이)
                Destroy(this.gameObject, itemDestroyTime);      // 오브젝트 지우기
            }
        }
    }


    private void Update()
    {
        if (spriteAlpha)            // 알파값 줄이기 (천천히 사라지는 애니메이션)
        {
            alpha -= Time.deltaTime;        // 초당 1씩 감소
            sprite.color = new Color(1, 1, 1, alpha);   // 알파값 재설정
            if(alpha < 0.0f)        // 알파 값이 0보다 작으면
            {
                spriteAlpha = false;        // 실행 중지
            }
        }
    }

    IEnumerator SetAlphaDlray()         // 알파 값 딜레이 코루틴
    {
        yield return new WaitForSeconds(4.0f);
        spriteAlpha = true;
    }
}
