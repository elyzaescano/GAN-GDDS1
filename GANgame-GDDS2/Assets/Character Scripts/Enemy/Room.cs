using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EnemyAI
{
    public class Room : MonoBehaviour 
    {
        public int roomNo;
        public float timeToSpawn;
        EnemySpawnManager esm;
        
        private void Start() {
            esm = FindObjectOfType<EnemySpawnManager>();
        }
        void Update()
        {
            if (esm.currentRoom.GetComponent<Room>().roomNo == roomNo)
            {
                StartCoroutine(SpawnCountdown());
            }
        }

        IEnumerator SpawnCountdown()
        {
            yield return new WaitForSeconds(timeToSpawn);

            EventManager.SpawnChecker();

            while (esm.currentRoom.GetComponent<Room>().roomNo != roomNo) //Supposedly pauses the coroutine 
            {
                yield return null;
            }
        }
    }
}