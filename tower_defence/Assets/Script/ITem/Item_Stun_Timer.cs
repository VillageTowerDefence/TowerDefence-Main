using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Stun_Timer : MonoBehaviour
{
    Item_Enemy_Stun_Use stun;
    int count = 0;

    private void Awake()
    {
        stun = GetComponentInParent<Item_Enemy_Stun_Use>();
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (count < 1)
            {
                StartCoroutine(stun.StartTimer());
                count++;
            }

        }
    }
}
