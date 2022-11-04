using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemBuffData : ScriptableObject
{
    public uint id = 0;                     // 아이템 ID
    public string itemName = "아이템";      // 아이템 이름
    public GameObject modelprefab;         // 아이템의 외형을 표시할 프리펩
    [Header("아이템 가격")]
    public uint value;                     // 아이템의 가격
    [Header("아이템 수량")]
    public uint Count = 0;                 // 소유중인 아이템 갯수
}
