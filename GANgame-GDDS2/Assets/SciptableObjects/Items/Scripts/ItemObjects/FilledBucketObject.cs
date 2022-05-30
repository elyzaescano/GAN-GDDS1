using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Filled Bucket Object", menuName = "InventorySystem/Items/FilledBucket")]
public class FilledBucketObject : ItemObject
{
    public void Awake()
    {
        type = Type.FilledBucket;
    }
}
