using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAI;

public class Trap : MonoBehaviour
{
    public GameObject Roomroom; //It's that time of the development cycle where I can't be bothered with naming conventions anymore
    Room room;
    void Start()
    {
        room = Roomroom.GetComponent<Room>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            EventManager.TriggerTrap();
            EventManager.TrapTriggered += ChangeSpawnTimer;
        }
    }

    void ChangeSpawnTimer()
    {
        print("Time Switch Success");
        room.setTimeToSpawn(0);
        EventManager.TrapTriggered -= ChangeSpawnTimer;
        Destroy(gameObject.GetComponent<Trap>());
    }
}
