using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    public GameObject bullet; // 공격 투사체

    public float attackSpeed = 1.0f; // 타워 공격 주기
    public float attackDamage; // 타워 공격력

    float originalAttackDamage;     // 타워 원래 데미지
    bool isphysics = false; // 물리/마법 타워 구별
    bool isOnBuffPower = false;      // 현재 파워아템 효과를 받고 있는지 (중첩 x)

    private List<GameObject> Enemys; // 적 List로 받기
    Transform target = null; // 공격 대상


    private void Awake()
    {
        Enemys = new List<GameObject>(); // 적 리스트 할당
    }

    private void Start()
    {
        StartCoroutine(PeriodAttack()); // 공격 코루틴 시작
        originalAttackDamage = attackDamage;
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

    public void BuffPowerUp(float power, bool onbuff)
    {
        if (onbuff)                     // 버프 받은 상태면
        {
            if (!isOnBuffPower)         // 현재 버프효과를 받고 있지 않으면
            {
                attackDamage *= power;  // 공격력 증가량 만큼 증가
                isOnBuffPower = true;   // powerUp 버프 받은 상태
            }
        }
        else                            // 버프 받은 상태 해제면
        {
            attackDamage = originalAttackDamage;        // 원래 데미지로 
            isOnBuffPower = false;      // powerUp 버프 해제 상태 
        }
       
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
}
