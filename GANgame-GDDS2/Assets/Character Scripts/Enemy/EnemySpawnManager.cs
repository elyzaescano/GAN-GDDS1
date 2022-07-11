using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EnemyAI
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public bool canSpawn;
        public GameObject enemy;
        public GameObject currentRoom;
        PlayerController player;
        private void Start() 
        {
            player = FindObjectOfType<PlayerController>();
            EventManager.RoomChanged += RoomChecker;
        }
        private void Update()
        {
            currentRoom = FindRoom();

            
        }

        void RoomChecker()
        {
            StartCoroutine(SpawnTimeManager());
        }

        IEnumerator SpawnTimeManager()
        {
            float spawnCountdown = currentRoom.GetComponent<Room>().timeToSpawn;

            print(spawnCountdown);
            yield return new WaitForSeconds(spawnCountdown);

            Instantiate(enemy, player.transform);
            canSpawn = false;
        }


        public GameObject FindRoom()
        {
            GameObject[] rooms;
            rooms = GameObject.FindGameObjectsWithTag("Room");

            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = player.transform.position;
            foreach (GameObject active in rooms)
            {
                Vector3 diff = active.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = active;
                    distance = curDistance;
                }
            }
            return closest;
        }
    }
}
