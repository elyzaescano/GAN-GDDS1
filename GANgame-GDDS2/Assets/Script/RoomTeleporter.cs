using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleporter : LockDoor
{
    public GameObject destGO;   //Destination GameObject
    private Vector2 destination;  //Destination location relative to worldpsace
    bool isTriggered = false;
    public EventManager em;
    public int doorID;
    int triggerID;
    GameObject player;

    LockDoor lockDoor;

    private void Start()
    {
        EventManager.DoorOpenEvent += Teleport;
        player = GameObject.FindWithTag("Player");
        destination = destGO.transform.position;
        lockDoor = FindObjectOfType<LockDoor>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            isTriggered = true;
            triggerID = player.GetComponent<PlayerController>().doorTriggerID = doorID;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isTriggered = false;
    }

    public void Teleport()
    {
        if (isTriggered && triggerID == doorID)
        {
            if (!lockDoor.isLocked)
            {
                player.transform.position = destination;
                print("Teleported to " + destination);
                isTriggered = false;
            }
        }
    }

    private void OnDisable()
    {
        EventManager.DoorOpenEvent -= Teleport;
    }

}
