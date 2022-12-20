using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Item : MonoBehaviour
{
    public ItemDataManager itemDataManager;
    public GameObject item_button;

    Button_Item[] button_Items;
    GameObject item_list;

    private void Awake()
    {
        item_list = transform.GetChild(0).GetChild(1).gameObject;
        Create_Item_Buttons();
        button_Items = item_list.GetComponentsInChildren<Button_Item>();
    }

    private void Start()
    {
        for (int i = 0; i < button_Items.Length; i++)
        {
            button_Items[i].ItemInitialize((uint)i);
            button_Items[i].Refresh();
        }

    }

    void Create_Item_Buttons()
    {
        for(int i = 0; i < itemDataManager.itemData.Length; i++)
        {
            GameObject item = Instantiate(item_button);
            item.transform.SetParent(item_list.transform);
        }
    }
}
