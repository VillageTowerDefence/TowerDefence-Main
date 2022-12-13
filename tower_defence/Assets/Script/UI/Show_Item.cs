using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Show_Item : MonoBehaviour
{
    public ItemData itemData;

    Image item_image;
    TextMeshProUGUI item_count;

    private void Awake()
    {
        item_image = transform.GetComponent<Image>();
        item_count = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        item_image.sprite = itemData.itemIcon;
        item_count.text = itemData.count.ToString();
    }
}
