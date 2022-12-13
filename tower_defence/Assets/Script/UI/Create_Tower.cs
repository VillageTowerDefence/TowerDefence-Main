using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Create_Tower : MonoBehaviour
{
    GameObject create_panel;
    Button cancel;
    Tile tile;

    public ObjectDetector detector;
    public TowerSpwaner towerSpwaner;

    int current_tower;
    float tower_radius = 150.0f;

    private void Awake()
    {
        create_panel = transform.GetChild(0).gameObject;
        cancel = create_panel.transform.GetChild(0).GetComponent<Button>();

        cancel.onClick.AddListener(OnClick_Cancel);

    }

    private void Start()
    {
        current_tower = create_panel.transform.childCount - 1;
        create_panel.SetActive(false);
    }

    private void Update()
    {
        if (detector.IsSelect && !create_panel.activeSelf)
        {
            if (detector.selectTile)
            {
                tile = detector.selectTile.GetComponent<Tile>();
                if (!tile.isBulidTower)
                {
                    Open_Panel();
                }
                else
                {
                    detector.IsSelect = false;
                    detector.selectTile = null;
                }
            }
        }
    }

    void Open_Panel()
    {
        create_panel.SetActive(true);
        create_panel.transform.position = Camera.main.WorldToScreenPoint(detector.selectTile.transform.position);
        for(int i = 0; i < current_tower; i++)
        {
            float tower_angle = Mathf.PI * 0.5f - i * (Mathf.PI * 2.0f) / current_tower;
            GameObject child_tower = create_panel.transform.GetChild(i + 1).gameObject;
            child_tower.transform.position = create_panel.transform.position + (new Vector3(Mathf.Cos(tower_angle), Mathf.Sin(tower_angle), 0)) * tower_radius;
        }
    }

    private void OnClick_Cancel()
    {
        create_panel.SetActive(false);
        detector.IsSelect = false;
        detector.selectTile = null;
    }

    public void OnClick_BuildButton(int towerIndex)
    {
        BuildTower(towerIndex);
        create_panel.SetActive(false);
    }


    void BuildTower(int index)
    {
        if (detector.selectTile != null && detector.IsSelect)
        {
            towerSpwaner.SpawnTower(detector.selectTile, index); // 버튼이 눌려지면 타워 스포너를 통해 설치
            detector.IsSelect = false; // 타일 해제
            detector.selectTile = null;
        }
    }

}
