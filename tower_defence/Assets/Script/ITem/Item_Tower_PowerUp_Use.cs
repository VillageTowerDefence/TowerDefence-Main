using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Tower_PowerUp_Use : Item_Buff
{
    public float time = 0.0f;
    public float damage = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            buff("Power");
            buffOn(this.gameObject, collision, time, damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            buffOff(collision);
        }
    }
}
