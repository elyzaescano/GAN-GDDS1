using System.Collections;
using System.Collections.Generic;
using System;
using Dialogue;
using UnityEngine;

namespace EnemyAI
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public bool canSpawn;
        public GameObject enemy;
        public GameObject currentRoom;
        PlayerController player;
        public GameObject dialogBox;
        public Conversation con_enemyspawn;
        public Conversation con_enemydeath;
        private void Start() 
        {
            player = FindObjectOfType<PlayerController>();
            canSpawn = true;

            dialogBox = EventManager.dialogBox;
            EventManager.EnemyCanSpawn += SpawnEnemy; //Subscribed!
            EventManager.EnemyDeath += DespawnEnemy;
            EventManager.OnEnemyDeath += PlayDeathConversation;

        }
        
        private void Update()
        {
            currentRoom = FindRoom();
        }

        void SpawnEnemy()
        {
            if (currentRoom != null) 
            {
             
                dialogBox.SetActive(true);
                DialogDisplay dd = dialogBox.GetComponentInChildren<DialogDisplay>();
                dd.conversation = con_enemyspawn;
                dd.simulateClick = true;
                
                Instantiate(enemy, currentRoom.GetComponent<Room>().spawnPoint);
                canSpawn = false;
                
                if(AudioManager.instance != null)
                AudioManager.instance.Play("Monster");
            }
        }

        public void DespawnEnemy()
        {
            canSpawn = true;
            EventManager.EnemyCanSpawn -= SpawnEnemy;
            EventManager.EnemyCanSpawn += SpawnEnemy;

            EventManager.AfterEnemyDied();
            EventManager.OnEnemyDeath += PlayDeathConversation;

            if(AudioManager.instance != null)
            AudioManager.instance.Stop("Monster");
        }

        public void PlayDeathConversation()
        {
            dialogBox.SetActive(true);
            DialogDisplay dd = dialogBox.GetComponentInChildren<DialogDisplay>();
            dd.conversation = con_enemydeath;
            dd.simulateClick = true;

            EventManager.OnEnemyDeath -= PlayDeathConversation;
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
