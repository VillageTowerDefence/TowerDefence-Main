using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectDetector : MonoBehaviour
{
    public TowerSpwaner towerSpwaner;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private PlayerInputAction controller;


    public GameObject selectTile;
    public GameObject selectTower;

    public bool isSelect = false; // 타일이 골라진다면


    private void Awake()
    {
        mainCamera = Camera.main;
        controller = new PlayerInputAction();
    }

    private void OnEnable()
    {
        controller.Enable();
        controller.Player.Build.performed += DetectTile;
        controller.Player.Build.canceled += DetectTile;
    }

    private void OnDisable()
    {
        controller.Player.Build.canceled -= DetectTile;
        controller.Player.Build.performed -= DetectTile;
        controller.Disable();
    }


    /// <summary>
    /// 타일 확인
    /// </summary>
    /// <param name="context"></param>
    private void DetectTile(InputAction.CallbackContext context)
    {
        if (!isSelect) // 선택된 타일이 없다면
        {
            if (context.performed) // 마우스가 눌릴때
            {
                ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()); //카메라 위치에서 마우스 클릭지점으로 향하는 광선
                RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

                if (hit2D.collider != null) //광선에 부딪히는 오브젝트 검출
                {
                    if (hit2D.transform.CompareTag("WallTile")) //WallTile이면
                    {
                        //towerSpwaner.SpawnTower(hit.transform); // 타워 설치
                        selectTile = hit2D.collider.gameObject; // 타일에 저장
                        
                        isSelect = true; // 타일이 선택되었다고 알려둠
                    }

                    if (hit2D.transform.CompareTag("Tower"))
                    {
                        selectTower = hit2D.collider.gameObject;
                        isSelect = true; // 타일이 선택되었다고 알려둠
                        
                    }
                }
            }
        }
    }
}
