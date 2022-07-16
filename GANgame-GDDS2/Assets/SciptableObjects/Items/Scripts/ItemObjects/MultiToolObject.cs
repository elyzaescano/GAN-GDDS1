using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MultiTool Object", menuName = "InventorySystem/Items/MultiTool")]
public class MultiToolObject : ItemObject
{
    public void Awake()
    {
        type = Type.MultiTool;
    }
}
