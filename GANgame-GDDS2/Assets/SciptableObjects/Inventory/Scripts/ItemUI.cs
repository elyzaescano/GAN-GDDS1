using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemObject item;
    private Image spriteImage;
    public InventoryObject playerInventory;
    public Image UI;
    public int itemSlotID;
    public bool selected = false;

    public PlayerController pc;

    // Start is called before the first frame update
    void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateImage(null);
        pc = FindObjectOfType<PlayerController>();
        InventoryUI uiScript = FindObjectOfType<InventoryUI>();
        UI = uiScript.GetComponent<Image>();
    }

    public void UpdateImage(ItemObject item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.sprite = this.item.itemIcon;
            if (UI.color == Color.white) spriteImage.color = Color.white; else if(UI.color == Color.clear) spriteImage.color = Color.clear;         
        }
        else { spriteImage.sprite = null; spriteImage.color = Color.clear; }
    }

    public void GiveItemObject()
    {
        if (!selected)
        {
            spriteImage.color = Color.black;
            ItemObject item = playerInventory.Container[itemSlotID].item;
            //print(item);
            pc.GetCraftingItems(item);
            selected = true;
        }
    }


}
