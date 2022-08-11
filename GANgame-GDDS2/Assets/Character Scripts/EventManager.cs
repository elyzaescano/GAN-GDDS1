using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public static GameObject dialogBox;

    public static event Action OpenInventory;
    public static event Action CloseInventory;
    public static event Action EquipItem;
    public static event Action Crafting;

    public static event Action MinigameCompleted;
    public static event Action SimonWon;
    public static event Action SimonFailed;

    public static event Action InteractEvent;

    public static event Action EnemyCanSpawn;
    public static event Action EnemyDeath;
    public static event Action GamePause;
    public static event Action GameResume;
    public static event Action EnemyWarning;

    //Room 4 house 2 Only
    public static event Action Room4DoorUnlock;

    private void Awake()
    {
        dialogBox = GameObject.FindGameObjectWithTag("Dialog").transform.GetChild(0).gameObject;
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

    public static void CraftingInvoke()
    {
        Crafting?.Invoke();

    }

    public static void ConquerMinigame()
    {
        MinigameCompleted?.Invoke();
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

    public static void PauseGame()//UNUSED
    {
        GamePause?.Invoke();
    }

    public static void ResumeGame()//UNUSED
    {
        GameResume?.Invoke();
    }

    public static void UnlockDoor4()
    {
        Room4DoorUnlock?.Invoke();
    }

    public static void WinSimon()
    {
        SimonWon?.Invoke();
    }
    public static void FailSimon()
    {
        SimonFailed?.Invoke();
    }

    public static void Warning() //UNUSED
    {
        EnemyWarning?.Invoke();
    }
}
