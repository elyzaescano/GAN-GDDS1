using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Key,Note,Diary,Tool,Potion,Sword,Default
}
    
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    public string itemName;
    public int id;
    [TextArea(15, 30)]
    public string description;
        
}
