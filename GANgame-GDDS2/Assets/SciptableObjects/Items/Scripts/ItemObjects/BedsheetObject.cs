using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bedsheet Object", menuName = "InventorySystem/Items/Bedsheet")]
public class BedsheetObject : ItemObject
{
    public void Awake()
    {
        type = Type.Bedsheet;
    }
}
