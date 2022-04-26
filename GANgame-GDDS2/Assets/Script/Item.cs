using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    Key,Note,Diary,Tool,Potion,Sword
}
    
[CreateAssetMenu(fileName ="New Item", menuName = "Gan/Item")]
public abstract class Item : ScriptableObject
{
    public GameObject prefab;
    public itemType type;
    public int id;
    public string itemName;
    public string description;
        
}
