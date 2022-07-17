using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stick Object", menuName = "InventorySystem/Items/Stick")]
public class StickObject : ItemObject
{
    public void Awake()
    {
        type = Type.Stick;
    }
}
