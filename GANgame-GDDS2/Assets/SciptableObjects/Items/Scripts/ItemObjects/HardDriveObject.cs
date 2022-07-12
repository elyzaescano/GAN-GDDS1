using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hard Drive Object", menuName = "InventorySystem/Items/HardDrive")]
public class HardDriveObject : ItemObject
{
    public void Awake()
    {
        type = Type.FloppyDisk;
    }
}
