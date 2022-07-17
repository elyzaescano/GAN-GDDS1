using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MoltenMetal Object", menuName = "InventorySystem/Items/MoltenMetal")]
public class MoltenMetalObject : ItemObject
{
    public void Awake()
    {
        type = Type.MoltenMetal;
    }
}
