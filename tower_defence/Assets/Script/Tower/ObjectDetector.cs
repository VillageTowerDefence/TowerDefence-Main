using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngineInternal;

public class ObjectDetector : MonoBehaviour
{
    public TowerSpwaner towerSpwaner;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private PlayerInputAction controller;


    public GameObject tile; 

    public bool isTileSelect = false; // 타일이 골라진다면

    private void Awake()
    {
        mainCamera = Camera.main;
        controller = new PlayerInputAction();
    }

    private void OnEnable()
    {
        controller.Enable();
        controller.Player.Build.performed += buildTower;
        controller.Player.Build.canceled += buildTower;
    }

    private void OnDisable()
    {
        controller.Player.Build.canceled -= buildTower;
        controller.Player.Build.performed -= buildTower;
        controller.Disable();
    }

    private void buildTower(InputAction.CallbackContext context)
    {
        if (!isTileSelect) // 선택된 타일이 없다면
        {
            if (context.performed) // 마우스가 눌릴때
            {
                ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()); //카메라 위치에서 마우스 클릭지점으로 향하는 광선


                if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //광선에 부딪히는 오브젝트 검출
                {
                    if (hit.transform.CompareTag("WallTile")) //WallTile이면
                    {
                        //towerSpwaner.SpawnTower(hit.transform); // 타워 설치
                        tile = hit.collider.gameObject; // 타일에 저장
                        isTileSelect = true; // 타일이 선택되었다고 알려둠
                    }
                }
            }
        }
    }
}
