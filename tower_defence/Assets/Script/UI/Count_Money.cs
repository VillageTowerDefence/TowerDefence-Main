using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count_Money : MonoBehaviour
{
    TextMeshProUGUI money_text;
    Manager_UI manager_UI;

    private void Awake()
    {
        money_text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        manager_UI = Manager_UI.Instance;

        manager_UI.refresh_Money = Refresh_Progress_Money;
    }

    void Refresh_Progress_Money()
    {
        //Debug.Log("갱신");
        money_text.text = manager_UI.Money_Count.ToString("D3");
    }
}
