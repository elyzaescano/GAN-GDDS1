using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName ="InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for(int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }

    //public void DropItem(ItemObject _item, int _amount)
    //{
    //    bool hasItem = true;
    //    for(int i = Container.Count-1 ; i < Container.Count; i--)
    //    {
    //        if(Container[i].item == _item)
    //        {
    //            Container[i].ReduceAmount(_amount);
    //            Container.RemoveAt(i);
    //            hasItem = false;
    //            break;
    //        }
    //    }
    //    if (!hasItem || Container.Count == 0)
    //    {
    //        Debug.Log("No Item");
    //    }
    //}
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;

    }
    public void AddAmount(int value)
    {
        amount += value;
    }

    public void ReduceAmount(int value)
    {
        amount -= value;
    }
}