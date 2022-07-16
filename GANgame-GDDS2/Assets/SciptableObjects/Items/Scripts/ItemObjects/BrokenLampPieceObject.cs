using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BrokenLampPiece Object", menuName = "InventorySystem/Items/BrokenLampPiece")]
public class BrokenLampPieceObject : ItemObject
{
    public void Awake()
    {
        type = Type.BrokenLampPiece;
    }
}
