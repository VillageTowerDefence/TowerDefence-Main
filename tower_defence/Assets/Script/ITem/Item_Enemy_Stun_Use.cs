using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Enemy_Stun_Use : MonoBehaviour
{
    [Header("기절시킬 적의 수 : 최대n명")]
    public int enemies = 1;
    List<GameObject> enemyList;
    int count = 0;

    private void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (count < enemies)
        {
            if (other.CompareTag("Enemy"))
            {
                enemyList.Add(other.gameObject);
                count++;
            }
        }
    }
}
