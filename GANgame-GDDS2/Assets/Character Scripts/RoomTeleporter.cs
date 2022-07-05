using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomTeleporter : LockDoor
{
    public GameObject destGO;   //Destination GameObject
    private Vector2 destination;  //Destination location relative to worldpsace
    bool isTriggered = false;
    public EventManager em;
    public int doorID;
    int triggerID;
    GameObject player;

    public AudioSource doorOpenClose;

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
                bool removeItem = itemRequired ? true : false;
                if (removeItem)
                {
                    StartCoroutine(removeEquippedItem());
                }
            }
        }
    }

    IEnumerator removeEquippedItem()
    {
        playerInventory.equippedItem = null;
        playerInventory.RemoveItem(playerInventory.equippedItem);
        yield return null;
        EventManager.ItemEquip();
    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= Teleport;
    }

}
