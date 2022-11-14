using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gage_HP : MonoBehaviour
{
    Image hp_gage;
    Manager_UI manager_UI;

    private void Awake()
    {
        hp_gage = transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        manager_UI = Manager_UI.Instance;

        GameManager.Instance.refresh_HP = Refresh_HP_Gage;
    }

    void Refresh_HP_Gage()
    {
        hp_gage.fillAmount = manager_UI.UI_Player_HP / manager_UI.UI_Player_HP_Max;
    }
}
