using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Create_Tower : MonoBehaviour
{
    GameObject create_panel;
    Button cancel;

    public ObjectDetector detector;
    public TowerSpwaner towerSpwaner;

    private void Awake()
    {
        create_panel = transform.GetChild(0).gameObject;
        cancel = create_panel.transform.GetChild(0).GetComponent<Button>();

        cancel.onClick.AddListener(OnClick_Cancel);

    }

    private void Start()
    {
        create_panel.SetActive(false);
    }

    private void Update()
    {
        if (detector.isSelect && !create_panel.activeSelf)
        {
            create_panel.SetActive(true);
            Debug.Log(detector.selectTile.transform.position);
            create_panel.transform.position = Camera.main.WorldToScreenPoint(detector.selectTile.transform.position);
        }
    }

    private void OnClick_Cancel()
    {
        create_panel.SetActive(false);
        detector.isSelect = false;
    }

    public void OnClick_BuildButton(int towerIndex)
    {
        BuildTower(towerIndex);
        create_panel.SetActive(false);
    }


    void BuildTower(int index)
    {
        if (detector.selectTile != null && detector.isSelect)
        {
            towerSpwaner.SpawnTower(detector.selectTile, index); // 버튼이 눌려지면 타워 스포너를 통해 설치
            detector.isSelect = false; // 타일 해제
        }
    }

}
