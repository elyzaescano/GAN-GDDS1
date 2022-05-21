using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rope Object", menuName = "InventorySystem/Items/Rope")]
public class RopeObject : ItemObject
{
    public void Awake()
    {
        type = Type.Rope;
    }
}
