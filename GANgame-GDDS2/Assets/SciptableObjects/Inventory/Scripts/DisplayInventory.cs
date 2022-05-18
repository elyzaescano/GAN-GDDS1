using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour       //CURRENTLY UNUSED. REFER TO InventoryUI FOR CURRENT SCRIPT 
{   //Manages UI Display for inventory
    public InventoryObject inventory;
    //I need to change these to Gridlayout after I figure out how to use it 
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMN;
    public Dictionary<InventorySlot, GameObject> itemsDisplay = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
        
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.UIprefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPos(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
        }
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplay.ContainsKey(inventory.Container[i]))
            {
                itemsDisplay[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                
            }
            else if(itemsDisplay.Count < inventory.Container.Count)
            {
                var obj = Instantiate(inventory.Container[i].item.UIprefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPos(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplay.Add(inventory.Container[i], obj);
            }
            //if (inventory.Container.Count < i) print("Less than shown");
            if (itemsDisplay.Count > inventory.Container.Count)
            {
                itemsDisplay.Remove(inventory.Container[i]);
            }
        }
        
    }

    public void RemoveInventorySlotUI(int i, ItemObject _item)
    {
        if(itemsDisplay.Count > inventory.Container.Count)
        {
            itemsDisplay.Remove(inventory.Container[i]);
        }
        itemsDisplay.Remove(inventory.Container[i]);
        //DestroyImmediate(obj,true);
    }

    public Vector3 GetPos(int i)
    {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEMS *(i%NUMBER_OF_COLUMN),Y_START + (-Y_SPACE_BETWEEN_ITEMS*(i/NUMBER_OF_COLUMN)), 0f);
    }
}
