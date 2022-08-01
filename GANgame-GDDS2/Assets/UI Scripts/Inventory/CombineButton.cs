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

    bool readyToCraft = false;
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
        if(pc.firstVariable != null && readyToCraft)
        {
            pc.secondVariable = obj;
            pc.secondItemUI = ui;
            StartCoroutine(CraftingCoroutine());
        }
        else
        {
            pc.firstVariable = obj;
            pc.firstItemUI = ui;
            //EventManager.Crafting += pc.GetCraftingItems;
            //EventManager.Crafting += ResetVariables;
        }



        //if (itemObjectsToCraft[0] == null)
        //{
        //    itemObjectsToCraft[0] = obj;
        //    uiVariables[0] = ui;
        //}
        //else
        //{
        //    itemObjectsToCraft[1] = obj;
        //    uiVariables[1] = ui;
        //}
    }
    IEnumerator CraftingCoroutine()
    {
        yield return new WaitForFixedUpdate();
        pc.GetCraftingItems();

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

    }

}
