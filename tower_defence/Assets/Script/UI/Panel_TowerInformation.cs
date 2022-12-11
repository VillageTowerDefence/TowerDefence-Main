using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel_TowerInformation : MonoBehaviour
{
    public ObjectDetector detector;

    GameObject panel_towerInfo;
    GameObject panel_closeButton;

    TextMeshProUGUI tower_name;
    TextMeshProUGUI tower_info;

    private void Awake()
    {
        panel_closeButton = transform.GetChild(0).gameObject;
        panel_towerInfo = transform.GetChild(1).gameObject;

        tower_name = panel_towerInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        tower_info = panel_towerInfo.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        panel_closeButton.SetActive(false);
        panel_towerInfo.SetActive(false);
    }
    private void Update()
    {
        if (detector.isSelect && !panel_towerInfo.activeSelf)
        {
            if (detector.selectTower && !detector.selectTile)
            {
                Open_Panel();
            }
        }
    }

    void Open_Panel()
    {
        panel_closeButton.SetActive(true);
        panel_towerInfo.SetActive(true);
        panel_towerInfo.transform.position = Camera.main.WorldToScreenPoint(detector.selectTower.transform.position);

        GameObject info_tower = detector.selectTower;
        tower_name.text = info_tower.name;
        //tower_info.text
    }

    public void Close_Panel()
    {
        detector.isSelect = false;
        detector.selectTower = null;
        panel_closeButton.SetActive(false);
        panel_towerInfo.SetActive(false);
    }
}
