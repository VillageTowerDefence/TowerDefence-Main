using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTower : Tower
{
    protected override void Awake()
    {
        base.Awake();
        towerUpgradeCost = new int[] { 100, 200, 300 };

    }

    public override void towerUpgrade()
    {
        if (GameManager.Instance.energy > towerUpgradeCost[level-1] && level <maxTowerLevel)
        {
            level++;
            attackDamage += 10;
            GameManager.Instance.energy -= towerUpgradeCost[level - 1];
        }
        else
        {
            Debug.Log("업그레이드 실패");
        }

        Debug.Log($"{level}   {attackDamage}");
    }


}
