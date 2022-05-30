using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Axe Object", menuName = "InventorySystem/Items/Axe")]
public class AxeObject : ItemObject
{
    public void Awake()
    {
        type = Type.Axe;
    }
}
