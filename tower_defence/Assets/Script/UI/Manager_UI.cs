using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager_UI : Singleton<Manager_UI>
{
    //일시정지
    bool game_pause_flag = false;
    public bool Game_Pause_Flag
    {
        get => game_pause_flag;
        private set
        {
            game_pause_flag = value;
        }
    }

    //게임 배속
    bool game_speedUp_flag = false;

    public bool Game_SpeedUP_Flag
    {
        get => game_speedUp_flag;
        private set
        {
            game_speedUp_flag = value;
        }
    }

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


    //진행도
    int game_progress_current = 0;
    int game_progress_max = 3;

    public int Game_Progress_Current
    {
        get => game_progress_current;
        private set
        {
            game_progress_current = value;
            refresh_Progress?.Invoke();
        }
    }

    public int Game_Progress_Max
    {
        get => game_progress_max;
    }

    public Action refresh_Progress;

    //에너지
    int energy_count = 0;
    
    public int Energy_Count
    {
        get => energy_count;
        private set
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

    protected override void Initialize()
    {
        Game_Pause_Flag = false;
        Game_SpeedUP_Flag = false;
        player_HP = player_HP_Max;
        Game_Progress_Current = 0;
        Energy_Count = 0;
        Money_Count = 0;
    }
    public void PauseGame()
    {
        Game_Pause_Flag = true;
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Game_Pause_Flag = false;
        Time.timeScale = 1;
    }

    public void SpeedUpGame()
    {
        Game_SpeedUP_Flag = true;
        Time.timeScale = 2.0f;
    }
    public void SpeedDownGame()
    {
        Game_SpeedUP_Flag = false;
        Time.timeScale = 1.0f;
    }

    public void Test_Damage_HP()
    {
        Player_HP--;
        Debug.Log(Player_HP + " / " + Player_HP_Max);
    }

    public void Test_UP_Progress()
    {
        if (Game_Progress_Current < Game_Progress_Max)
        {
            Game_Progress_Current++;
            Debug.Log(Game_Progress_Current + " / " + Game_Progress_Max);
        }
    }

    public void Test_UP_Energy()
    {
        Energy_Count++;
        Debug.Log(Energy_Count);
    }

    public void Test_UP_Money()
    {
        Money_Count++;
        Debug.Log(Money_Count);
    }
}
