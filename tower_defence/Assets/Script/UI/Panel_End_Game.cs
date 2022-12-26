using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel_End_Game : MonoBehaviour
{
    GameObject panel_clear;
    GameObject panel_over;
    Manager_UI manager_UI;

    private void Start()
    {
        panel_clear = transform.GetChild(0).gameObject;
        panel_over = transform.GetChild(1).gameObject;
        manager_UI = Manager_UI.Instance;
        manager_UI.refresh_Progress += Game_Clear;
        GameManager.Instance.refresh_HP += Game_Over;
        panel_clear.SetActive(false);
        panel_over.SetActive(false);
    }

    void Game_Clear()
    {
        if (manager_UI.Game_Progress_Current == manager_UI.Game_Progress_Max && !panel_over.activeSelf)
        {
            panel_clear.SetActive(true);
        }
    }
    void Game_Over()
    {
        if (manager_UI.UI_Player_HP <= 0 && !panel_clear.activeSelf)
        {
            panel_over.SetActive(true);
        }
    }

    public void Click_NewGame_Button()
    {
        Manager_Scene manager_Scene = new Manager_Scene();
        manager_Scene.moveScene(SceneManager.GetActiveScene().name);
    }

    public void Click_ExitGame_Button()
    {
        Manager_Scene manager_Scene = new Manager_Scene();
        manager_Scene.moveScene("UI_MainScene");
    }
}
