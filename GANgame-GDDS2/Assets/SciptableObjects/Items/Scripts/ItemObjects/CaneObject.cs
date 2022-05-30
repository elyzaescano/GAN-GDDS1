using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cane Object", menuName = "InventorySystem/Items/Cane")]
public class CaneObject : ItemObject
{
    public void Awake()
    {
        type = Type.Cane;
    }
}
