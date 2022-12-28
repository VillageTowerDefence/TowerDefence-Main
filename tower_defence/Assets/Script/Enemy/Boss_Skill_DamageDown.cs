using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_DamageDown : MonoBehaviour
{
    [Range(0f, 1f)]
    public float reduceDamage;          // 데미지 감소율
    public float skillCoolTime;         // 스킬 쿨타임
    public float skillDuration;         // 스킬 지속시간

    BuffType tpye = BuffType.PowerDown;
    BuffManager buffManager;

    private void Start()
    {
        buffManager = GameManager.Instance.Buff;
        StartCoroutine(SkillStart());
    }

    IEnumerator SkillStart()
    {
        while (true)
        {
            Tower[] towers = FindObjectsOfType<Tower>(); 

            yield return new WaitForSeconds(skillCoolTime);

            foreach (var tower in towers)
            {
                buffManager.CreateBuff(tpye, reduceDamage, skillDuration, tower);
            }
        }
    }
}
