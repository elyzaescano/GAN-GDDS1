using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object", menuName = "InventorySystem/Items/Key")]
public class KeyObject : ItemObject
{
    public int damage;
    public void Awake()
    {
        type = ItemType.Key;
    }
}
