using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemUI : MonoBehaviour
{
    public EventManager em;
    public InventoryObject playerInventory;
    Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        itemImage = GetComponentInChildren<Image>();
        EventManager.EquipItem += UpdateItemImage;
    }

    public void UpdateItemImage()
    {
        if (playerInventory.equippedItem != null)
        {
            Sprite newImage = playerInventory.equippedItem.itemIcon;
            if (itemImage != null) { 
            itemImage.sprite = newImage;
            itemImage.color = Color.white;
            }
            return;
        }
        itemImage.sprite = null;
        itemImage.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
