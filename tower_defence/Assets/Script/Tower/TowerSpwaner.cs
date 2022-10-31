using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TowerSpwaner : MonoBehaviour
{
    public GameObject[] towers; // 타워를 배열로 받음


    public void SpawnTower(GameObject tileTransform,int index)
    {
        
        Tile tile = tileTransform.GetComponent<Tile>();
        if (!tile.isBulidTower)
        {
            Instantiate(towers[index], tileTransform.transform.position, Quaternion.identity);
            tile.isBulidTower = true;
        }
        
    }
}
