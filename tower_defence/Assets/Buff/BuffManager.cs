using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public GameObject buffPrefab;

    public void CreateBuff(BuffType type, float value, float time, Tower tower)
    {
        GameObject obj = Instantiate(buffPrefab, transform);
        obj.GetComponent<BuffBase>().Initialize(type, value, time, tower);
    }

    public void CreateBuff(BuffType type, float value, float time, Enemy enemy)
    {
        GameObject obj = Instantiate(buffPrefab, transform);
        obj.GetComponent<BuffBase>().Initialize(type, value, time, enemy);
    }
}
