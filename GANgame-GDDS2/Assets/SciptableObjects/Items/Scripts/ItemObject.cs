using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


    
public abstract class ItemObject : ScriptableObject
{
    
    public enum Type //elyza says enums are just thingamabobs (aka values) that STORE INTEGERS. Add item here EVERY SINGLE TIME there's a new item
    {
        Key, Bedsheet, Hairpin, ScrapMetal, Adapter, Cable, Timber, Rope, Lockpick, PhonePlug, Bucket, Matches, Cane, FloppyDisk, Axe, FilledBucket, Battery, Casette, WoodenBaseBallBat, 
        Lighter, HairSpray, Ink, Foil, MoltenMetal, MultiTool, Stick, BrokenLampPiece, FlameThrower, DuctTape,
        Painting ,Potion, Sword, Default
    }

    public Sprite itemIcon;
    public GameObject itemPrefab;
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

    public ItemObject Combine(ItemObject reagent) //elyza says Responsible for making the recipes work. Ignore unless there's a bug that needs a fixin'
    {
        for(int i = 0;i < recipes.Length; i++) {
            if (recipes[i].other == reagent) return recipes[i].result;
            
        }
        return null;
    }
        
}
