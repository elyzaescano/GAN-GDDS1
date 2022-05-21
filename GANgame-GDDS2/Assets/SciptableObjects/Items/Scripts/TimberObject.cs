using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Timber Object", menuName = "InventorySystem/Items/Timber")]
public class TimberObject : ItemObject
{
    private void Awake()
    {
        type = Type.Timber;
    }
}
