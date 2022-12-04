using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Life_Down : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        Hp = hp / 2;    // hp 절반
    }
}
