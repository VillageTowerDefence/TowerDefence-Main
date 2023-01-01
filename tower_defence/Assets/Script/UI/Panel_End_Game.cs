using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel_End_Game : MonoBehaviour
{
    public GameObject panel_clear;
    public GameObject panel_over;
    Manager_UI manager_UI;

    private void Awake()
    {
        manager_UI = Manager_UI.Instance;
    }

    private void Start()
    {
        manager_UI.refresh_Progress += Game_Clear;
        GameManager.Instance.refresh_HP += Game_Over;
        panel_clear.SetActive(false);
        panel_over.SetActive(false);
    }

    void Game_Clear()
    {
        if (manager_UI.Game_Progress_Current == manager_UI.Game_Progress_Max)
        {
            if((panel_over != null) && (!panel_over.activeSelf))
            {
                panel_clear.SetActive(true);
            }
        }
    }
    void Game_Over()
    {
        if (manager_UI.UI_Player_HP <= 0 && !panel_clear.activeSelf)
        {
            if ((panel_clear != null) && (!panel_clear.activeSelf))
            {
                panel_over.SetActive(true);
            }
        }
    }

    void Unconnect_Action()
    {
        GameManager.Instance.refresh_HP -= Game_Over;
        manager_UI.refresh_Progress -= Game_Clear;
    }


    public void Click_NewGame_Button()
    {
        manager_UI.manager_Scene.moveScene(SceneManager.GetActiveScene().name);
        Unconnect_Action();
    }

    public void Click_ExitGame_Button()
    {
        manager_UI.manager_Scene.moveScene("UI_MainScene");
        Unconnect_Action();
    }
}
