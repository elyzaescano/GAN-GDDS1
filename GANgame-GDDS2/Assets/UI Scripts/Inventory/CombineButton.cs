using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CombineButton : MonoBehaviour
{
    public ItemObject[] itemObjects;

    public ItemUI[] uiVariables;


    private static PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        EventManager.OpenInventory += ResetVariables;

    }

    public void UpdateItemVariables(ItemObject obj, ItemUI ui)
    {
        if (itemObjects[0] == null)
        {
            itemObjects[0] = obj;
            uiVariables[0] = ui;
        }
        else
        {
            itemObjects[1] = obj;
            uiVariables[1] = ui;
        }
    }

    public void ResetVariables()
    {
        for (int i = 0; i < itemObjects.Length; i++)
        {
            itemObjects[i] = null;
        }
        for (int i = 0; i < uiVariables.Length; i++)
        {
            uiVariables[i] = null;
        }
    }

    public void CombineButtonMethod()
    {
        pc.GetCraftingItems(itemObjects[0], itemObjects[1], uiVariables[0], uiVariables[1]);

        for(int i = 0; i < itemObjects.Length; i++)
        {
            itemObjects[i] = null;
        }
        for (int i = 0; i < uiVariables.Length; i++)
        {
            uiVariables[i].selected = false;
            uiVariables[i] = null;
        }
    }

}
