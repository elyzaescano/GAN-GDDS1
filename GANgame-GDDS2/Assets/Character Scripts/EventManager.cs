using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action RoomEnterEvent;

    public static event Action OpenInventory;
    public static event Action CloseInventory;
    public static event Action EquipItem;

    public static event Action PaintingCompleted;

    public static event Action InteractEvent;

    
  


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

    public static void CompletePainting() //UNUSED
    {
        PaintingCompleted?.Invoke();
    }

    public static void RoomEntered(int roomID){
        RoomEnterEvent?.Invoke();
    }

    public static void Interact()
    {

        InteractEvent?.Invoke();
    }

}
