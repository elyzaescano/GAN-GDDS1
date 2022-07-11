using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using EnemyAI;

public class RoomTeleporter : LockDoor
{
    public GameObject destGO;   //Destination GameObject
    private Vector2 destination;  //Destination location relative to worldpsace
    bool isTriggered = false;
    public EventManager em;
    public int doorID;
    public float spawnCountdown;
    int triggerID;
    GameObject player;

    public AudioSource doorOpenClose;
    EnemySpawnManager esm;
    LockDoor lockDoor;

    //checks if player has the item 
    public InventoryObject playerInventory;
    public ItemObject itemRequired;
    public bool itemNeeded = false; //if we need any key

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        destination = destGO.transform.position;
        lockDoor = FindObjectOfType<LockDoor>();
        EventManager.EnterRoomEvent += Teleport;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            EventManager.InteractEvent += Teleport;
            ItemObject o = playerInventory.equippedItem;
            if (o == itemRequired || itemRequired == null)
            {
                itemNeeded = true;
                lockDoor.isLocked = false;
            }

            isTriggered = true;
            triggerID = player.GetComponent<PlayerController>().doorTriggerID = doorID;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EventManager.InteractEvent -= Teleport;
        isTriggered = false;
    }

    public void Teleport()
    {
        if (isTriggered && triggerID == doorID)
        {
            if (!lockDoor.isLocked && itemNeeded)
            {
                doorOpenClose.Play();
                player.transform.position = destination;
                print("Teleported to " + destination);
                isTriggered = false;

                //Invokes the event for the EnemySpawn
                EventManager.EnterRoom();
            }
        }
    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= Teleport;
    }

}
