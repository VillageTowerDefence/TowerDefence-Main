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

    // 타워 상태 ---------------------------------------------------------------------------

    public float attackSpeed = 1.0f; // 타워 공격 주기
    public float attackDamage; // 타워 공격력
    float originalAttackDamage;     // 타워 원래 데미지
    float originalAttackSpeed;      // 타워 원래 공격 주기

    bool isphysics = false; // 물리/마법 타워 구별
    bool isAttackFly = false; // 공중 공격 가능 true 가능 false 불가

    protected int costenergy; // 타워 건설 에너지
    protected int level; //타워 레벨
    protected int maxTowerLevel = 3;
    protected int[] towerUpgradeCost;


    // 타워 승급 ------------------------------------------------------------------------------------

    public GameObject advanceTower = null;


    // 아이템 ------------------------------------------------------------------------------
    public List<BuffBase> onBuff;
    // ------------------------------------------------------------------------------------

    private List<GameObject> Enemys; // 적 List로 받기
    Transform target = null; // 공격 대상

    // 프로퍼티 -------------------------------------------------------------------------

    public int CostEnergy => costenergy;

    public int Level => level;

    public int MaxTowerLevel => maxTowerLevel;


    protected virtual void Awake()
    {
        Enemys = new List<GameObject>(); // 적 리스트 할당
        level = 1;

        // 아이템 관련 ----------------------------------------------------------
        onBuff = new List<BuffBase>();
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


    /// <summary>
    /// 타워 공격 함수
    /// </summary>
    protected virtual void Attack()
    {
        target = Enemys[0].transform; //리스트의 첫번째 적에게 공격
        Vector3 targetDir = (target.position - transform.position).normalized; // 방향을 구한후
        float angle = Vector2.SignedAngle(Vector2.right, targetDir); // 각도 구하기

        GameObject obj = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))); // 총알을 적의 방향으로 생성
        Bullet bull = obj.GetComponent<Bullet>();
        bull.Power = attackDamage; // 총알에 데메지 전달
        bull.IsPhysics = isphysics; // 총알에 물리/마법 속성 전달

        obj.transform.parent = this.transform; // 총알을 타워의 자식으로 설정
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

    public virtual void towerUpgrade()
    {
        
    }

    // 버프 관련 함수 ----------------------------------------------------------------------------------------------

    /// <summary>
    /// 버프 on / off하는 함수
    /// </summary>
    /// <param name="Type">버프의 타입</param>
    /// <param name="origin">오리지날 상태 수치</param>
    /// <returns></returns>
    float BuffChange(BuffType Type, float origin)
    {
        if(onBuff.Count > 0)
        {
            bool buffCheck = false;             // 자기 자신을 찾았는지 확인하는 변수
            float temp = 0;
            for (int i = 0; i < onBuff.Count; i++)
            {
                if (onBuff[i].buffType.Equals(Type))
                {
                    buffCheck = true;
                    temp += origin * onBuff[i].percentage;      // 해당 버프의 퍼센트(%) 수치만큼 적용
                    break;
                }
            }

            if (!buffCheck)         // 자기 자신을 찾지 못 했을 겅우
            {
                // 버프 해제
                return origin;      // 원래 상태로 돌아가기
            }
            else
            {
                // 버프 적용
                return temp;        // 자기 자신을 찾았을 경우, 그 효과로 적용
            }
        }
        else
        {
            // 적용 받은 버프가 없으면 원래 상태로
            return origin;
        }
    }

    /// <summary>
    /// 버프를 선택하는 함수
    /// </summary>
    /// <param name="type">버프의 타입</param>
    public void ChooseBuff(BuffType type)
    {
        switch (type)
        {
            case BuffType.Power:
                attackDamage = BuffChange(type, originalAttackDamage);
                break;
            case BuffType.AttactSpeed:
                attackSpeed = BuffChange(type, originalAttackSpeed);
                break;
            case BuffType.Slow:
                break;
            case BuffType.Stun:
                break;
            default:
                break;
        }
    }
    // --------------------------------------------------------------------------------------------------------------

    // 타워 승급 -----------------------------------------------------------------------------------

    public void towerAdvance()
    {
        if(advanceTower != null)
        {
            Instantiate(advanceTower,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    // 타워 시너지 ---------------------------------------------------------------------------------------
    protected void TowerSynergy()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 3.0f, LayerMask.GetMask(""));      // 범위 정하기, 레이어로 분류할 것인지...
        // 시너지 변수 = collider.Length;
    }
}
