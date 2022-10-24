using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.UIElements;

public class Tower : MonoBehaviour
{
    public GameObject bullet; // 공격 투사체

    public float attackSpeed = 1.0f; // 타워 공격 주기
    public float attackDamage; // 타워 공격력
    float originalAttackDamage;     // 타워 원래 데미지
    float originalAttackSpeed;      // 타워 원래 공격 주기

    bool isphysics = false; // 물리/마법 타워 구별

    // 아이템 ------------------------------------------------------------------------------
    bool[] isOnBuffPower;      // 현재 파워아템 효과를 받고 있는지 (중첩 x)
    int[] buffEA;                  // 현재 버프가 몇개 겹쳤는지 확인
    string[] buffKind = { "Power", "ActtackSpeed" };        // 버프 종류
    // ------------------------------------------------------------------------------------

    private List<GameObject> Enemys; // 적 List로 받기
    Transform target = null; // 공격 대상


    private void Awake()
    {
        Enemys = new List<GameObject>(); // 적 리스트 할당

        // 아이템 관련 ----------------------------------------------------------
        isOnBuffPower = new bool[buffKind.Length];
        for (int i = 0; i < buffKind.Length; i++)
        {
            isOnBuffPower[i] = false;
        }

        buffEA = new int[buffKind.Length];
        for (int i = 0; i < buffKind.Length; i++)
        {
            buffEA[i] = 0;
        }
        //-----------------------------------------------------------------------
    }

    private void Start()
    {
        StartCoroutine(PeriodAttack()); // 공격 코루틴 시작
        originalAttackDamage = attackDamage;
        originalAttackSpeed = attackSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // 적을 만날때
        {
            Enemys.Add(collision.gameObject); // 적을 리스트에 추가
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) //적이 나갈때
        {
            foreach (GameObject enemy in Enemys)
            {
                if (enemy == collision.gameObject) // 나간 적이 리스트에 있다면
                {
                    Enemys.Remove(enemy); //그 적을 리스트에 제거
                    break;
                }
            }
        }
    }

    private void Attack()
    {
        target = Enemys[0].transform; //리스트의 첫번째 적에게 공격
        Vector3 targetDir = (target.position - transform.position).normalized; // 방향을 구한후
        float angle = Vector2.SignedAngle(Vector2.right, targetDir); // 각도 구하기

        Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))); // 총알을 적의 방향으로 생성
    }

    IEnumerator PeriodAttack()
    {
        while (true)
        {
            if (Enemys.Count != 0) //적이 있다면
            {
                Attack(); //공격을 시작
            }
            yield return new WaitForSeconds(attackSpeed); // 공격주기
        }
    }

    // 버프 관련 함수 ----------------------------------------------------------------------------------------------
    public void BuffOn(float value, bool onbuff, string buffName)
    {
        int index = 0;                          // 버프 효과 인덱스

        switch (buffName)
        {
            case "Power":
                index = 0;
                if (BuffOverlap(value, onbuff, index))
                {
                    attackDamage *= value;  // 공격력 증가량 만큼 증가
                }
                if (buffEA[index] == 0)
                {
                    attackDamage = originalAttackDamage;        // 원래 공격력으로 복귀
                }
                break;
            case "AttackSpeed":
                index = 1;
                if (BuffOverlap(value, onbuff, index))
                {
                    // 0.0f ~ 1.0f
                    attackSpeed = attackSpeed - (attackSpeed * value);      // 일정 수치 만큼 공격속도 증가
                }
                if (buffEA[index] == 0)
                {
                    attackSpeed = originalAttackSpeed;           // 원래 공격속도로 복귀
                }
                break;
            default:
                break;
        }
    }

    bool BuffOverlap(float value, bool onbuff, int index)
    {
        bool result = false;
        if (onbuff)                     // 버프 받은 상태면 (공격력 증가 효과를 중첩으로 받지 않게 하기 위함)
        {
            if (!isOnBuffPower[index])         // 현재 버프효과를 받고 있지 않으면
            {
                isOnBuffPower[index] = true;   // powerUp 버프 받은 상태
                result = true;
            }
            buffEA[index]++;                   // 중첩된 갯수 증가
        }
        else                            // 버프 받은 상태 해제면
        {
            buffEA[index]--;                   // 중첩된 갯수 감소
            if (buffEA[index] == 0)
            {
                isOnBuffPower[index] = false;      // powerUp 버프 해제 상태 
                buffEA[index] = 0;
            }
        }
        return result;
    }
    // --------------------------------------------------------------------------------------------------------------
}
