using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HairSpray Object", menuName = "InventorySystem/Items/HairSpray")]
public class HairSprayObject : ItemObject
{
    public void Awake()
    {
        type = Type.HairSpray;
    }
}
