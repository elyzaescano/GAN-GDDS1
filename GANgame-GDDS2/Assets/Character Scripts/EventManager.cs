using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public static event Action OpenInventory;
    public static event Action CloseInventory;
    public static event Action EquipItem;

    public static event Action PaintingCompleted;

    public static event Action InteractEvent;

    public static event Action EnemyCanSpawn;
    public static event Action EnemyDeath;
    public static event Action GamePause;
    public static event Action GameResume;

    //Room 4 house 2 Only
    public static event Action Room4DoorUnlock;

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

    public static void Interact()
    {
        InteractEvent?.Invoke();
    }

    public static void SpawnChecker()
    {
        EnemyCanSpawn?.Invoke();
    }

    public static void EnemyDied()
    {
        EnemyDeath?.Invoke();
    }

    public static void PauseGame()
    {
        GamePause?.Invoke();
    }

    public static void ResumeGame()
    {
        GameResume?.Invoke();
    }

    public static void UnlockDoor4()
    {
        Room4DoorUnlock?.Invoke();
    }
   
}
