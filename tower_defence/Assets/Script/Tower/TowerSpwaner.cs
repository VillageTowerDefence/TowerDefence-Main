using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpwaner : MonoBehaviour
{
    public GameObject tower;

    public void SpawnTower(Transform tileTransform)
    {
        Instantiate(tower, tileTransform.position, Quaternion.identity);
    }
}
