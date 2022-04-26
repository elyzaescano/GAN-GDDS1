using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Default Object", menuName ="InventorySystem/Items/Default")]
public class PotionObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Potion;
    }
}
