using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DuctTape Object", menuName = "InventorySystem/Items/DuctTape")]
public class DuctTapeObject : ItemObject
{
    public void Awake()
    {
        type = Type.DuctTape;
    }
}
