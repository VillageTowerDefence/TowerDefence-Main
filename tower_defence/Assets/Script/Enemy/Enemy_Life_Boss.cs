using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Life_Boss : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        Hp = hp * 10;    // hp 절반
    }
}
