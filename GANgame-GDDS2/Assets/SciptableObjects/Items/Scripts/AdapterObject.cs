using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Adapter Object", menuName = "InventorySystem/Items/Adapter")]
public class AdapterObject : ItemObject
{
    public void Awake()
    {
        type = Type.Adapter;
    }
}
