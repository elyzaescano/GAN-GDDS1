using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action DoorOpenEvent;

    public static event Action OpenInventory;

    public static event Action CloseInventory;

    public static event Action EquipItem;
  
    public static void DoorOpen()
    {
        DoorOpenEvent?.Invoke();
    }

    public static void InventoryToggle()
    {
        OpenInventory?.Invoke();
    }

    public static void InventoryClose()     //UNUSED
    {
        CloseInventory?.Invoke();
    }

    public static void ItemEquip()
    {
        EquipItem?.Invoke();
    }

}
