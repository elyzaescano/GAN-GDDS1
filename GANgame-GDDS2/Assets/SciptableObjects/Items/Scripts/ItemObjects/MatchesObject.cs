using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Matches Object", menuName = "InventorySystem/Items/Matches")]
public class MatchesObject : ItemObject
{
    public void Awake()
    {
        type = Type.Matches;
    }
}
