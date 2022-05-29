using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //Creates a list of InventorySlots in the UI. Limit has to be the same as InventorySlots
    public List<ItemUI> _itemUI = new List<ItemUI>(10);
    public GameObject slotPrefab;
    public Transform slotPanel;
    public InventoryButton IB;
    public int numberOfSlots;

    public InventoryObject inventory;

    // Start is called before the first frame update
    void Awake()
    {
        numberOfSlots = _itemUI.Capacity;
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            _itemUI.Add(instance.GetComponentInChildren<ItemUI>());
            instance.GetComponentInChildren<ItemUI>().itemSlotID = i;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSlot(int _slot, ItemObject _item)
    {      
            _itemUI[_slot].UpdateImage(_item);
    }
    public void AddNewItem(ItemObject _item)
    {
        UpdateSlot(_itemUI.FindIndex(i => i.item == null), _item);
    }
    public void RemoveItem(ItemObject _item)
    {
        UpdateSlot(_itemUI.FindIndex(i => i.item == _item), null);
        print("removed" + _item);
    }

    //Ok. I spent like 3 hours figuring out how to reload the UI. This was the only way that worked from the POV of a beginner programmer, thank you.

    public void UpdateFullUI(InventorySlot _is)
    {
        for (int i = 0; i < _is.amount; i++)
        {
            AddNewItem(_is.item);
        }
    }
 

    public IEnumerator UpdateUIFromLoad(InventorySlot _is)
    {
        foreach(ItemUI iu in _itemUI)
        {
            iu.UpdateImage(null);
        }
        yield return 1;
        UpdateFullUI(_is);
    }



}
