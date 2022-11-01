using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Count_Energy : MonoBehaviour
{
    TextMeshProUGUI energy_text;
    Manager_UI manager_UI;

    private void Awake()
    {
        energy_text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        manager_UI = Manager_UI.Instance;

        manager_UI.refresh_Energy = Refresh_Progress_Energy;
    }

    void Refresh_Progress_Energy()
    {
        //Debug.Log("갱신");
        energy_text.text = manager_UI.Energy_Count.ToString("D3");
    }
}
