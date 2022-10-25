using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpwaner : MonoBehaviour
{
    public GameObject[] towers;


    public void SpawnTower(GameObject tileTransform,int index)
    {
        Instantiate(towers[index], tileTransform.transform.position, Quaternion.identity);

        
    }
}
