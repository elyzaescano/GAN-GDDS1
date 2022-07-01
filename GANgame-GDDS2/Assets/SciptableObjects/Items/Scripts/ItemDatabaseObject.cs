using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Imma be honest with u. I followed the tutorial and it added this. I have no idea what this is for. I guess it's for getting the entire
//                                                                                                  list of items rendered in game for it to be accessed to later.
//elyza says Hakeem is correct probably
[CreateAssetMenu(fileName = "New Database Object", menuName = "InventorySystem/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{   //Item Database that stores ID for reference in serialization
    public ItemObject[] items;
    public Dictionary<ItemObject, int> GetID = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {   //Adds Item ID to dictionary
        GetID = new Dictionary<ItemObject, int>();
        GetItem = new Dictionary<int, ItemObject>();
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
