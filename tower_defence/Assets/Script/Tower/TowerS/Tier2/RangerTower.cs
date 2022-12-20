using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerTower : ArcherTower
{
    Transform[] targets;
    int targetAttackCount =3;

    protected override void Start()
    {
        targets = new Transform[targetAttackCount];
        base.Start();
    }
    protected override void Attack()
    {
        for (int i = 0; i < targetAttackCount; i++)
        {
            if (Enemys.Count > i)
            {
                targets[i] = Enemys[i].transform; // 타겟 카운트만큼 지정
                Vector3 targetDir = (targets[i].position - transform.position).normalized; // 방향을 구한후
                float angle = Vector2.SignedAngle(Vector2.right, targetDir); // 각도 구하기

                GameObject obj = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))); // 총알을 적의 방향으로 생성
                Bullet bull = obj.GetComponent<Bullet>();
                bull.Damage = attackDamage; // 총알에 데메지 전달
                bull.IsPhysics = isphysics; // 총알에 물리/마법 속성 전달
                bull.Target = Enemys[i];

                obj.transform.parent = this.transform; // 총알을 타워의 자식으로 설정
            }
        }
    }
}
