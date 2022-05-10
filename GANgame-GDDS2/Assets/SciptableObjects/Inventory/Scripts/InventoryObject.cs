using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName ="InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    public ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();


    public void AddItem(ItemObject _item, int _amount)
    {

        for(int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }     
        Container.Add(new InventorySlot(database.GetId[_item],_item, _amount));
        
    }

    public void OnAfterDeserialize()    //Saves item ID's after deserialize
    {
        for(int i = 0; i < Container.Count; i++) Container[i].item = database.GetItem[Container[i].ID];
        
    }

    public void OnBeforeSerialize()
    {
    }
    public void Save()
    {

    }
    public void Load()
    {

    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;

    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
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