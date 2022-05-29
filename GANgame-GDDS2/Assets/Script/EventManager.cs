using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action DoorOpenEvent;

    public static event Action OpenInventory;

    public static event Action CloseInventory;


    public static void DoorOpen()
    {
        DoorOpenEvent?.Invoke();
    }

    public static void InventoryOpen()
    {
        OpenInventory?.Invoke();
    }

    public static void InventoryClose()
    {
        CloseInventory?.Invoke();
    }

}
