using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FlameThrower Object", menuName = "InventorySystem/Items/FlameThrower")]
public class FlameThrowerObject : ItemObject
{
    public void Awake()
    {
        type = Type.FlameThrower;
    }
}
