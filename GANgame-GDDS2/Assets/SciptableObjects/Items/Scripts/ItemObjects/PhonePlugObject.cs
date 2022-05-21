using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Phone Plug Object", menuName = "InventorySystem/Items/PhonePlug")]
public class PhonePlugObject : ItemObject
{
    public void Awake()
    {
        type = Type.PhonePlug;
    }
}
