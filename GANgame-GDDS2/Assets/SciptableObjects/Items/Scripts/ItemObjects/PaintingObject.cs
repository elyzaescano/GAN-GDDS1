using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Painting Object", menuName = "InventorySystem/Items/Painting")]
public class PaintingObject : ItemObject
{
    public void Awake()
    {
        type = Type.Painting;
    }
}
