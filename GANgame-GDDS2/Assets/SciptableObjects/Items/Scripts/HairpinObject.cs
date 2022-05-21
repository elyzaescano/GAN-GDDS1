using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hairpin Object", menuName = "InventorySystem/Items/Hairpin")]
public class HairpinObject : ItemObject
{
    public void Awake()
    {
        type = Type.Hairpin;
    }
}
