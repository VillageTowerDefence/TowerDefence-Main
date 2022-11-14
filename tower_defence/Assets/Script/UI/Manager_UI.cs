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
    public float UI_Player_HP
    {
        get => GameManager.Instance.Player_HP;
    }

    public float UI_Player_HP_Max
    {
        get => GameManager.Instance.Player_HP_Max;
    }


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
    public int UI_Energy_Count
    {
        get => GameManager.Instance.Energy_Count;
    }

    //돈
    public int UI_Money_Count
    {
        get => GameManager.Instance.Money_Count;
    }

    protected override void Initialize()
    {
        Game_Pause_Flag = false;
        Game_SpeedUP_Flag = false;
        Game_Progress_Current = 0;
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

    //public void Test_Damage_HP()
    //{
    //    Player_HP--;
    //    Debug.Log(Player_HP + " / " + Player_HP_Max);
    //}

    public void Test_UP_Progress()
    {
        if (Game_Progress_Current < Game_Progress_Max)
        {
            Game_Progress_Current++;
            Debug.Log(Game_Progress_Current + " / " + Game_Progress_Max);
        }
    }

    //public void Test_UP_Energy()
    //{
    //    Energy_Count++;
    //    Debug.Log(Energy_Count);
    //}

    //public void Test_UP_Money()
    //{
    //    Money_Count++;
    //    Debug.Log(Money_Count);
    //}
}
