using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    public GameObject destGO;   //Destination GameObject
    private Vector2 destination;  //Destination location relative to worldpsace
    bool isTriggered = false;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        destination = destGO.transform.position;
    }
    private void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = destination;
            print("Teleported to " + destination);
            isTriggered = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            isTriggered = true;
        }
    }

}
