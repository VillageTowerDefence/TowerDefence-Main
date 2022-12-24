using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Boss_Skill_Stun : MonoBehaviour
{
    [Header("스턴 지속 시간")]
    public float stunTime = 5.0f;
    [Header("스턴 범위")]
    public float stunRange = 2.0f;
    public BuffType type;
    List<Tower> towers;

    BuffManager buffManager;

    //Animator anim;
    CircleCollider2D col;

    private void Awake()
    {
        buffManager = GameManager.Instance.Buff;
        col = GetComponent<CircleCollider2D>();
        col.radius = stunRange;
        col.isTrigger = true;
        //anim = GetComponent<Animator>();
        towers = new List<Tower>(64);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            towers.Add(collision.GetComponent<Tower>());
        }
    }

    public IEnumerator StartTimer()
    {
        //anim.SetBool("TimerEnd", true);             // 애니메이션 작동
        yield return new WaitForSeconds(1.0f);      // 1초 뒤에
        //anim.SetBool("TimerEnd", false);            // 애니메이션 중지
        col.enabled = false;                        // 콜라이더 끄기
        foreach (var Tower in towers)
        {
            buffManager.CreateBuff(type, 0, stunTime, Tower);                 // 적한테 스턴 적용
        }
    }
    //IEnumerator HpHeal()
    //{
    //    Enemy enemy = gameobjForHP.GetComponent<Enemy>();
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(10.0f);                       // 10초마다
    //        gameobjForHP.GetComponent<Enemy>().Hp += (enemy.MaxHP / 4);    // 최대 hp의 4분의1 회복
    //        Debug.Log("HP가 MaxHP/4만큼 회복");
    //    }
    //}
}
