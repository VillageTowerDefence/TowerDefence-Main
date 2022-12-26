using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicianTower : Tower
{
    protected override void Awake()
    {
        base.Awake();
        towerUpgradeCost = new int[] { 200, 400, 600 };
        isphysics = false;
        isAttackFly = true;
    }

    public override void towerUpgrade()
    {
        if (GameManager.Instance.energy > towerUpgradeCost[level - 1] && level < maxTowerLevel)
        {
            level++;
            attackDamage += 10;
            GameManager.Instance.energy -= towerUpgradeCost[level - 1];
        }
        else
        {
            Debug.Log("업그레이드 실패");
        }

        Debug.Log($"towerlevel {level}  attackDamage {attackDamage}");
    }
}
