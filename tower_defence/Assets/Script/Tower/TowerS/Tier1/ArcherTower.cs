using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArcherTower : Tower
{
    

    protected override void Awake()
    {
        base.Awake();
        towerUpgradeCost = new int[] { 50, 100, 150 };

    }

    protected override void Start()
    {
        base.Start();
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

        Debug.Log($"{level}   {attackDamage}");
    }

    
}
