using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Battery Object", menuName = "InventorySystem/Items/Battery")]

public class BatteryObject : ItemObject
{
    public void Awake()
    {
        type = Type.Battery;
    }
}
