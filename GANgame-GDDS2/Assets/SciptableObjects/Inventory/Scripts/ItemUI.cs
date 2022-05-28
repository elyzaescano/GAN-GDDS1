using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemObject item;
    private Image spriteImage;
    public InventoryObject playerInventory;
    public int itemSlotID;
    public bool selected = false;

    public PlayerController pc;

    // Start is called before the first frame update
    void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateImage(null);
        pc = FindObjectOfType<PlayerController>();
    }

    public void UpdateImage(ItemObject item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.itemIcon;
        }
        else { spriteImage.color = Color.clear; }
    }

    public void GiveItemObject()
    {
        if (!selected)
        {
            ItemObject item = playerInventory.Container[itemSlotID].item;
            print(item);
            pc.GetCraftingItems(item);
            selected = true;
        }
    }


}
