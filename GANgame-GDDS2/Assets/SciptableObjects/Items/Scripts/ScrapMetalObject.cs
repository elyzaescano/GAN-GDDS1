using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scrap Metal Object", menuName = "InventorySystem/Items/ScrapMetal")]
public class ScrapMetalObject : ItemObject
{
    public void Awake()
    {
        type = Type.ScrapMetal;
    }
}
