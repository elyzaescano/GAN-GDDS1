using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<ItemUI> _itemUI = new List<ItemUI>(10);
    public GameObject slotPrefab;
    public Transform slotPanel;
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
}
