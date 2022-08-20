using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CombineButton : MonoBehaviour
{
    public ItemObject tempItem;
    public ItemObject[] itemObjectsToCraft;

    public ItemUI[] uiVariables;

    public static bool readyToCraft = false;
    private static PlayerController pc;
    public EventManager em;

    private void Awake()
    {
        em = FindObjectOfType<EventManager>();
        pc = FindObjectOfType<PlayerController>();
        //EventManager.OpenInventory += ResetVariables;
    }


    public void UpdateItemVariables(ItemObject obj, ItemUI ui)
    {
        if(readyToCraft && itemObjectsToCraft[0]!=null)
        {
            itemObjectsToCraft[1] = obj;
            uiVariables[1] = ui;
            StartCoroutine(CraftingCoroutine());
        }
        else
        {
            itemObjectsToCraft[0] = obj;
            uiVariables[0] = ui;
            //EventManager.Crafting += pc.GetCraftingItems;
            //EventManager.Crafting += ResetVariables;
        }

    }
    IEnumerator CraftingCoroutine()
    {
        pc.firstVariable = itemObjectsToCraft[0];
        pc.secondVariable = itemObjectsToCraft[1];
        pc.firstItemUI = uiVariables[0];
        pc.secondItemUI = uiVariables[1];
        yield return new WaitForFixedUpdate();
        pc.GetCraftingItems();
        yield return null;
        ResetVariables();

    }
    public void ResetVariables()
    {
        for (int i = 0; i < itemObjectsToCraft.Length; i++)
        {
            itemObjectsToCraft[i] = null;
        }
        for (int i = 0; i < uiVariables.Length; i++)
        {
            uiVariables[i].selected = false;
            uiVariables[i] = null;
        }
    }

    public void CombineButtonMethod()
    {
        readyToCraft = true;
     
    }


    private void OnDisable()
    {
        EventManager.Crafting -= pc.GetCraftingItems;
        EventManager.Crafting -= ResetVariables;
        for(int i = 0; i<itemObjectsToCraft.Length; i++)
        {
            itemObjectsToCraft[i] = null;
        }
        for(int i = 0; i<uiVariables.Length; i++)
        {
            uiVariables[i] = null;
        }
        readyToCraft = false;
    }

}
