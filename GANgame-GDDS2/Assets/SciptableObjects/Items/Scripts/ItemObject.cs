using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
public abstract class ItemObject : ScriptableObject
{

    public enum Type
    {
        Key, Note, Diary, Tool, Potion, Sword, Default
    }

    public Sprite itemIcon;
    public GameObject UIprefab;
    public GameObject objectPrefab;
    public Type type;
    public string itemName;
    public int id;
    [TextArea(15, 30)]
    public string description;

    [System.Serializable]
    public struct Recipe
    {
        public ItemObject other;
        public ItemObject result;
    }
    public Recipe[] recipes; 

    public ItemObject Combine(ItemObject reagent)
    {
        for(int i = 0;i < recipes.Length; i++) {
            if (recipes[i].other == reagent) return recipes[i].result;
        }
        return null;
    }
        
}
