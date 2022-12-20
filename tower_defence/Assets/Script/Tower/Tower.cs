using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


#if UNITY_EDITOR
using UnityEditor;
#endif
public class Tower : MonoBehaviour
{
    public GameObject bullet; // 공격 투사체

    // 타워 상태 ---------------------------------------------------------------------------

    public TowerType tpye = TowerType.Archer;
    public float attackSpeed = 1.0f; // 타워 공격 주기
    public float attackDamage = 50.0f; // 타워 공격력
    float originalAttackDamage;     // 타워 원래 데미지
    float synergyDamage;
    float originalAttackSpeed;      // 타워 원래 공격 주기
    float buffDamage;

    protected bool isphysics = false; // 물리/마법 타워 구별
    protected bool isAttackFly = false; // 공중 공격 가능 true 가능 false 불가

    protected int costenergy; // 타워 건설 에너지
    protected int level; //타워 레벨
    protected int maxTowerLevel = 3;
    protected int[] towerUpgradeCost;


    // 타워 승급 ------------------------------------------------------------------------------------

    public GameObject advanceTower = null;


    // 아이템 ------------------------------------------------------------------------------
    public List<BuffBase> onBuff;
    // ------------------------------------------------------------------------------------

    protected List<GameObject> Enemys; // 적 List로 받기
    protected Transform target = null; // 공격 대상

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

    protected virtual void Start()
    {
        StartCoroutine(PeriodAttack()); // 공격 코루틴 시작
        originalAttackDamage = attackDamage;
        originalAttackSpeed = attackSpeed;
        synergyDamage = 1.0f;
        buffDamage = 1.0f;
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.IsPressed())
        {
            TowerSynergy();
        }
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
        bull.Damage = attackDamage; // 총알에 데메지 전달
        bull.IsPhysics = isphysics; // 총알에 물리/마법 속성 전달
        bull.Target = Enemys[0];

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
        if (onBuff.Count > 0)
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
                buffDamage = BuffChange(type, originalAttackDamage) * 0.01f;
                TowerStateUpdate();
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
        if (advanceTower != null)
        {
            Instantiate(advanceTower, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    // 타워 시너지 ---------------------------------------------------------------------------------------
    public void TowerSynergy()
    {
        int count = 0;
        List<Collider2D> widhtCollider = new List<Collider2D>(Physics2D.OverlapBoxAll(transform.position, new Vector2(3.0f, 1.0f), 0, LayerMask.GetMask("Tower")));
        List<Collider2D> heightCollider = new List<Collider2D>(Physics2D.OverlapBoxAll(transform.position, new Vector2(1.0f, 3.0f), 0, LayerMask.GetMask("Tower")));
        widhtCollider.Remove(this.GetComponent<Collider2D>());
        heightCollider.Remove(this.GetComponent<Collider2D>());
        if (widhtCollider != null || heightCollider != null)
        {
            Tower nearTower;
            if (widhtCollider.Count > 0)
            {
                foreach (var tower in widhtCollider)
                {
                    nearTower = tower.GetComponent<Tower>();
                    if (tpye == nearTower.tpye)
                    {
                        count++;
                    }
                }
            }

            if (heightCollider.Count > 0)
            {
                foreach (var tower in heightCollider)
                {
                    nearTower = tower.GetComponent<Tower>();
                    if (tpye == nearTower.tpye)
                    {
                        count++;
                    }
                }
            }


        }
        synergyDamage = ((float)count * 0.5f) + 1.0f;
        TowerStateUpdate();
        Debug.Log($"{this.name}주변 십자의 타워 : {count}개");
    }

    void TowerStateUpdate()
    {
        attackDamage = originalAttackDamage * synergyDamage * buffDamage;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(3.0f, 1.0f));
        Gizmos.DrawWireCube(this.transform.position, new Vector3(1.0f, 3.0f));
    }
#endif
}
