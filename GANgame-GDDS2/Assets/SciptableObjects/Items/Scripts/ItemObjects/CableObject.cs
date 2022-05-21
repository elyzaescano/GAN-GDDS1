using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cable Object", menuName = "InventorySystem/Items/Cable")]
public class CableObject : ItemObject
{
    public void Awake()
    {
        type = Type.Cable;
    }
}
