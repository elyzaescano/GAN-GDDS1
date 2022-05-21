using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;


//Creates and handles item management in the player's inventory.
[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    [SerializeField]
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>(10);

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    public void AddItem(ItemObject _item, int _amount) //Adds item to player inventory
    {
        //For loop that checks if item already exists in inventory,
        //Then adds to amount if item is already in inventory.
        for (int i = 0; i < Container.Count; i++) 
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount); //If item already exists, adds to the amount 
                return;
            }
        }
        //If item is not already in inventory
        //Adds an inventory slot and passes item variables to constructor
        Container.Add(new InventorySlot(database.GetID[_item], _item, _amount)); 
        Debug.Log("added " + _item);
    }

    public void DropItem(int _is, Vector2 pos) 
    {
        //Get's an ItemObject from GetItemObject(), Then drops the item.
        ItemObject IO = GetItemObject(_is);
        Debug.Log(Container[_is].amount);
        Instantiate(IO.itemPrefab, new Vector2(pos.x, pos.y - 1.5f), Quaternion.identity);
        Container[_is].ReduceAmount(1);
       
    }

    public ItemObject GetItemObject(int i)//gets item id in inventory and returns it
    {
        //The keypress passes an integer into this function,
        //it then returns an ItemObject corresponding to it's order in the inventory
        ItemObject _io = Container[i].item;     
        Debug.Log("Getting " + _io);

        //If there is no item, returns a null. 
        //If there is item, returns ItemObject.
        if (_io != null)
            return _io;
        else { Debug.Log("nth in this bish"); return null;  }
    }


    public void RemoveItem(int i)   //Ridiculously inefficient way to remove an item from the inventory. But it works
    {
        foreach(InventorySlot _is in Container)
        {
            if(_is.ID == i)
            {
                if(_is.amount > 1)
                {
                    _is.ReduceAmount(1);
                }
                else
                {
                    Container.Remove(_is);
                    break;
                }
            }
        }
    }

    //Save-Load system for inventory. Tbh I don't quite get it either.
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(fs, saveData);
        fs.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(fs).ToString(), this);
            fs.Close();
        }
    }


    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++) Container[i].item = database.GetItem[Container[i].ID];

    }

    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]
public class InventorySlot  //Inventory class
{
    public int ID;
    public ItemObject item;
    public int amount;

    public InventorySlot(int _id, ItemObject _item, int _amount) //Item constructor
    {
        item = _item;
        amount = _amount;
        ID = _id;
        Debug.Log("New " + _item + " Created");
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