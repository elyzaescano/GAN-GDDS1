using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Phone Book Object", menuName = "InventorySystem/Items/PhoneBook")]
public class PhoneBookObject : ItemObject
{
    private void Awake()
    {
        type = Type.PhoneBook;
    }
}
