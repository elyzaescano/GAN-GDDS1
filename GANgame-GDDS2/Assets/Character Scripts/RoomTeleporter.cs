using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using EnemyAI;
using Dialogue;



[System.Serializable]
public class RoomTeleporter : LockDoor
{
    public GameObject destGO;   //Destination GameObject
    private Vector3 destination;  //Destination location relative to worldpsace
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
    [SerializeField]bool _useKey;
    [SerializeField] ItemObject.Type type;

    //Dialog variables
    GameObject dialog;
    public Conversation conversation;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<InventoryObject>();
        destination = destGO.transform.position;
        lockDoor = FindObjectOfType<LockDoor>();
        dialog = EventManager.dialogBox;
        if(itemRequired !=null)_useKey = itemRequired.type == type;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            ItemObject o = playerInventory.equippedItem;
            if (o == itemRequired || itemRequired == null)
            {
                EventManager.InteractEvent += Teleport;
                itemNeeded = true;
                lockDoor.isLocked = false;
            }
            else
            {
                EventManager.InteractEvent += PlayDialog;
            }

            isTriggered = true;
            triggerID = player.GetComponent<PlayerController>().doorTriggerID = doorID;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EventManager.InteractEvent -= Teleport;
        EventManager.InteractEvent -= PlayDialog;
        isTriggered = false;
    }

    public void Teleport()
    {
        if (isTriggered && triggerID == doorID)
        {
            if (!lockDoor.isLocked && itemNeeded)
            {
                doorOpenClose.Play();
                destination.z = player.transform.position.z;
                player.transform.position = destination;
                print("Teleported to " + destination);
                isTriggered = false;
                if (_useKey)
                {
                    playerInventory.RemoveItem(playerInventory.equippedItem);
                    playerInventory.equippedItem = null;
                    EventManager.ItemEquip();
                    itemRequired = null;
                    _useKey = false;
                    //Trigger feedback of item break here
                }
            }
        }
    }

    public void PlayDialog()
    {
        dialog.SetActive(true);
        DialogDisplay dd = dialog.GetComponent<DialogDisplay>();

        dd.conversation = conversation;
        dd.simulateClick = true;

        EventManager.InteractEvent -= PlayDialog;

    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= Teleport;
        EventManager.InteractEvent -= PlayDialog;
    }

}
