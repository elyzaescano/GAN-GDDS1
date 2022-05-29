using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public InventoryUI inventoryPanel;
    public GameObject slotPanel;
    Image panelImage;

    public void Start()
    {
        panelImage = inventoryPanel.gameObject.GetComponent<Image>();
        StartCoroutine(SettingInventoryVisibilityOnStart());

    }
    bool isActive = false;

    public void OpenInventory()
    {
        if(inventoryPanel != null)
        {
            isActive = !isActive; //Makes inventory UI toggleable

            if (isActive) StartCoroutine(SettingInventoryVisibilityOnStart()); else if (!isActive) StartCoroutine(MakingInventoryVisible()); //Activates the inventory UI
        }
    }

    [SerializeField] Button[] e;
    public IEnumerator SettingInventoryVisibilityOnStart()
    {
        
        panelImage.color = Color.clear;

        yield return 1;
        
        foreach (Transform _is in slotPanel.transform)
        {
            if (_is.name.StartsWith("ItemSlot"))
            {
                Image slotImage = _is.GetComponent<Image>();
                slotImage.color = Color.clear;
                print(slotImage.color);
                e = _is.GetComponentsInChildren<Button>();
                foreach(Button butt in e)
                {
                    butt.gameObject.SetActive(false);
                }
            }
        }
        yield return 1;
        foreach (ItemUI _im in inventoryPanel._itemUI)
        {
            Image spriteImage = _im.GetComponent<Image>();
            spriteImage.color = Color.clear;        }
    }

    public IEnumerator MakingInventoryVisible()
    {
        panelImage.color = Color.white;

        yield return 1;

        foreach (Transform _is in slotPanel.transform)
        {
            if (_is.name.StartsWith("ItemSlot"))
            {
                Image slotImage = _is.GetComponent<Image>();
                slotImage.color = Color.white;
                print(slotImage.color);
                e = _is.GetComponentsInChildren<Button>();
                foreach (Button butt in e)
                {
                    butt.gameObject.SetActive(true);
                }
            }
        }
        yield return 1;
        foreach (ItemUI _im in inventoryPanel._itemUI)
        {
            Image spriteImage = _im.GetComponent<Image>();
            if (spriteImage.sprite == null) spriteImage.color = Color.clear;
            else 
            spriteImage.color = Color.white;
        }
    }

}
