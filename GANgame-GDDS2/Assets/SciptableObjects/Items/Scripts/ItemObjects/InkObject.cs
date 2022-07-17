using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ink Object", menuName = "InventorySystem/Items/Ink")]
public class InkObject : ItemObject
{
    public void Awake()
    {
        type = Type.Ink;
    }
}
