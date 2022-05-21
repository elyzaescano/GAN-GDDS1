using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bucket Object", menuName = "InventorySystem/Items/Bucket")]
public class BucketObject : ItemObject
{
    public void Awake()
    {
        type = Type.Bucket;
    }
}
