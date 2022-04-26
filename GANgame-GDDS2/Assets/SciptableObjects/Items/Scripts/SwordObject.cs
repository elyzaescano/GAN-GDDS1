using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "InventorySystem/Items/Sword")]
public class SwordObject : ItemObject
{
    public int damage;
    public void Awake()
    {
        type = ItemType.Sword;
    }
}
