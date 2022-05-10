using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Databse Object", menuName = "InventorySystem/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] items;
    public Dictionary<ItemObject, int> GetID = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();


    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<ItemObject, int>();
        for(int i = 0; i < items.Length; i++)
        {
            GetID.Add(items[i], i);

            GetItem.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {

        
    }


}
