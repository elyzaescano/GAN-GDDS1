using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Foil Object", menuName = "InventorySystem/Items/Foil")]
public class FoilObject : ItemObject
{
    public void Awake()
    {
        type = Type.Foil;
    }
}
