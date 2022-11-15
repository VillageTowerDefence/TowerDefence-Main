using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Click_Item : MonoBehaviour
{
    public GameObject buy_panel;

    public void Open_Buy_Panel()
    {
        GameObject gameObject = transform.gameObject;
        buy_panel.GetComponent<Button_Buy_Item>().buy_item = gameObject;
        buy_panel.SetActive(true);
    }
}
