using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{

    //체력 관련
    float player_HP = 10.0f;
    float player_HP_Max = 10.0f;

    public float Player_HP
    {
        get => player_HP;
        private set
        {
            player_HP = value;
            refresh_HP?.Invoke();
        }
    }

    public float Player_HP_Max
    {
        get => player_HP_Max;
    }

    public Action refresh_HP;

    //에너지
    int energy_count = 0;

    public int Energy_Count
    {
        get => energy_count;
        set
        {
            energy_count = value;
            refresh_Energy?.Invoke();
        }
    }

    public Action refresh_Energy;

    //돈
    int money_count = 0;

    public int Money_Count
    {
        get => money_count;
        private set
        {
            money_count = value;
            refresh_Money?.Invoke();
        }
    }

    public Action refresh_Money;


    public int energy = 1000;

    ItemDataManager itemData;

    BuffManager buff;

    /// <summary>
    /// 아이템 데이터 메니저 (읽기 전용) 프로퍼티
    /// </summary>
    public ItemDataManager ItemDta => itemData;

    /// <summary>
    /// 버프 메니저 (읽기 전용) 프로퍼티
    /// </summary>
    public BuffManager Buff => buff;
    protected override void Initialize()
    {
        player_HP = player_HP_Max;
        Energy_Count = 0;
        Money_Count = 0;
        itemData = GetComponent<ItemDataManager>();
        buff = GetComponent<BuffManager>();
    }
}
