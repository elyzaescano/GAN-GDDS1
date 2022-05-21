using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lockpick Object", menuName = "InventorySystem/Items/Lockpick")]
public class LockpickObject : ItemObject
{
    public void Awake()
    {
        type = Type.Lockpick;
    }
}
