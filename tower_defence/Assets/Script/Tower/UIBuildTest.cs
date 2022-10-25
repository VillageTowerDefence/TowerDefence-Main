using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildTest : MonoBehaviour
{
    Button buildButton1;
    Button buildButton2;
    Button buildButton3;


    public ObjectDetector detector;
    public TowerSpwaner towerSpwaner;
    int towerIndex;

    private void Awake()
    {
        buildButton1 = transform.GetChild(0).GetComponent<Button>();
        buildButton2 = transform.GetChild(1).GetComponent<Button>();
        buildButton3 = transform.GetChild(2).GetComponent<Button>();

        buildButton1.onClick.AddListener(OnClick_BuildButton1);
        buildButton2.onClick.AddListener(OnClick_BuildButton2);
        buildButton3.onClick.AddListener(OnClick_BuildButton3);

        towerIndex = 0;
    }

    void OnClick_BuildButton1()
    {
        towerIndex = 0;
        BuildTower(towerIndex);
    }

    void OnClick_BuildButton2()
    {
        towerIndex = 1;
        BuildTower(towerIndex);
    }
    void OnClick_BuildButton3()
    {
        towerIndex = 2;
        BuildTower(towerIndex);
    }

    void BuildTower(int index)
    {
        if (detector.tile != null)
        {
            towerSpwaner.SpawnTower(detector.tile, index);
            detector.isTowerSelect = false;
        }
    }
}
