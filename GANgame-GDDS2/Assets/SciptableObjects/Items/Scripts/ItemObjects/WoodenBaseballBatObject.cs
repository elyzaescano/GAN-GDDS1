using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WoodenBaseballBat Object", menuName = "InventorySystem/Items/WoodenBaseballBat")]
public class WoodenBaseballBatObject : ItemObject
{
    public void Awake()
    {
        type = Type.WoodenBaseBallBat;
    }
}
