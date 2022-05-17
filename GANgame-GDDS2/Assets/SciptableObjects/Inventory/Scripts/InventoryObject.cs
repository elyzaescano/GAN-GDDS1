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
    public List<InventorySlot> Container = new List<InventorySlot>();

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
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount); //If item already exists, adds to the amount 
                return;
            }
        }
        Container.Add(new InventorySlot(database.GetID[_item], _item, _amount)); //Adds an inventory slot and passes item variables to constructor

    }

    public void DropItem(int _is, Vector2 pos) //Drops item. NOTE: Currently may still work even if item is below 0
    {
        ItemObject IO = GetItemObject(_is);
        Debug.Log(Container[_is].amount);
        //Broken lol: doesnt instantiate correct item based on order in inventory
        Instantiate(IO.objectPrefab, new Vector2(pos.x, pos.y - 1.5f), Quaternion.identity);
        Container[_is].ReduceAmount(1);
       
    }

    public ItemObject GetItemObject(int i)//gets item id in inventory and returns it
    {
        ItemObject _io = Container[i].item;     
        Debug.Log(_io);
        return _io;
    }

    public void CombineItem(int _reactor, int _reagent)
    {
        ItemObject reactor = GetItemObject(_reactor);
        ItemObject reagent = GetItemObject(_reagent);
        ItemObject result = reactor.Combine(reagent);

        Container.Add(new InventorySlot(result.id, result, 1));
        Container[_reactor].ReduceAmount(1);Container[_reagent].ReduceAmount(1);
    }


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