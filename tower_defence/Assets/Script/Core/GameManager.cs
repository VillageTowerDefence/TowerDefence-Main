using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int energy = 1000;

    ItemDataManager itemData;

    /// <summary>
    /// 아이템 데이터 메니저 (읽기 전용) 프로퍼티
    /// </summary>
    public ItemDataManager ItemDta => itemData;

    protected override void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();
    }
}
