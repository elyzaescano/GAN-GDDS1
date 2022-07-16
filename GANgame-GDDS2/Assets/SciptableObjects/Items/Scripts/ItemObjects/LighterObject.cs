using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lighter Object", menuName = "InventorySystem/Items/Lighter")]
public class LighterObject : ItemObject
{
    public void Awake()
    {
        type = Type.Lighter;
    }
}
