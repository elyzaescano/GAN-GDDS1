using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

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
    public void AddItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(database.GetID[_item], _item, _amount));

    }

    public void DropItem(int _is, Vector2 pos) //Drops item. NOTE: Currently may still work even if item is below 0
    {
        int i = GetItemID(_is);
        Instantiate(database.items[i].objectPrefab, new Vector2(pos.x + 2, pos.y - 4), Quaternion.identity);
        Container[i].ReduceAmount(1);
    }

    public int GetItemID(int i)//gets item id in inventory and returns it
    {
        int id = Container[i].ID;
        Debug.Log(id);
        return id;
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
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;

    public InventorySlot(int _id, ItemObject _item, int _amount)
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