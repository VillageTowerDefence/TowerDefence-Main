using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinghtTower : WarriorTower
{
    protected override void Awake()
    {
        base.Awake();
        towerUpgradeCost = new int[] { 50, 100, 150 };
        isphysics = true;
        isAttackFly = false;
    }
}
