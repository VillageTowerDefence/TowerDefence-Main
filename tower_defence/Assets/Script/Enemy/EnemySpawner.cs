using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      //  enemy 프리펩

    public float spawnTime;             // 스폰 시간

    public Transform[] wayPoints;       // 현재 스테이지 이동 경로

    private void Start()
    {
        StartCoroutine(SpawnEnemy());   // SpawnEnemy코루틴 시작
    }
    
    IEnumerator SpawnEnemy()
    {
        if (enemyPrefab.GetComponent<Enemy_Spawn_Count_Up>() != null)
        {
            GameManager.Instance.Enemy_Spawn_Count *= 2;
        }
        else if (enemyPrefab.GetComponent<Enemy_Spawn_Count_Down>() != null)
        {
            GameManager.Instance.Enemy_Spawn_Count -= 5;
        }
        else if (enemyPrefab.GetComponent<Enemy_Spawn_Count_Boss>() != null)
        {
            GameManager.Instance.Enemy_Spawn_Count = 1;
        }
        else
        {
            GameManager.Instance.Enemy_Spawn_Count = 10;
        }
        for ( int i=0; i< GameManager.Instance.Enemy_Spawn_Count; i++)
        {
            GameObject obj = Instantiate(enemyPrefab);  // obj에 enemyPrefab오브젝트 생성
            Enemy enemy = obj.GetComponent<Enemy>();        // obj에 생성된 적의 enemy 컴포넌트

            enemy.Setup(wayPoints);         // wayPoints 정보를 매개변수로 Setup() 호출

            yield return new WaitForSeconds(spawnTime);     // spawnTime 시간 동안 대기
        }
    }

}
