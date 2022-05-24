using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    public Vector2 destination;  //other door 
    bool isTriggered = false;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = destination;
            print("Teleported to " + destination);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTriggered = true;
        }
    }

}
