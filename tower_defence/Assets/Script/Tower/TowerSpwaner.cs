using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpwaner : MonoBehaviour
{
    public GameObject[] towers; // 타워를 배열로 받음


    public void SpawnTower(GameObject tileTransform,int index)
    {
        Instantiate(towers[index], tileTransform.transform.position, Quaternion.identity);

        
    }
}
