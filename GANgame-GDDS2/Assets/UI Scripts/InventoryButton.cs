using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void OpenInventory()
    {
        if(inventoryPanel != null)
        {
            bool isActive = inventoryPanel.activeSelf; //Makes inventory UI toggleable

            inventoryPanel.SetActive(!isActive); //Activates the inventory UI
        }
    }

}
