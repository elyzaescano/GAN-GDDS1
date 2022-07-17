using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Casette Object", menuName = "InventorySystem/Items/Casette")]
public class CasetteObject : ItemObject
{
    public void Awake()
    {
        type = Type.Casette;
    }
}
