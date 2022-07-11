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
        RoomTeleporter roomTele;


        private void Start() 
        {
            roomTele = FindObjectOfType<RoomTeleporter>();
            EventManager.EnterRoomEvent += SpawnManager;
        }

        public void SpawnManager()
        {
                StartCoroutine(SpawnTimer());
        }

        IEnumerator SpawnTimer()
        {   
            yield return new WaitForSeconds(roomTele.spawnCountdown);    
            print(roomTele.spawnCountdown);
        }

        void ActuallySpawn()
        {
            
        }
    }
}
