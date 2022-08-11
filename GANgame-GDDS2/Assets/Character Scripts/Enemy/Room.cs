using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace EnemyAI
{
    public class Room : MonoBehaviour 
    {
        public int roomNo;
        public float timeToSpawn;
        float originalTime;
        public Transform spawnPoint;
        PauseScreen pause;
        EnemySpawnManager esm;
        public AudioSource warning;
       
        
        
        private void Start() 
        {
            esm = FindObjectOfType<EnemySpawnManager>();
         
            spawnPoint = transform.GetChild(0).transform;
            originalTime = timeToSpawn;
            //EventManager.EnemyWarning += EWarning;
            pause = FindObjectOfType<PauseScreen>();
        }
        void Update()
        {
            if (esm.currentRoom.GetComponent<Room>().roomNo == roomNo && esm.canSpawn && !pause.isPaused) //roomNo. value + enemy can spawn + is not paused
            {
                timeToSpawn -= Time.deltaTime; //timetospawn = ~ - time.deltatime
            }
            else
            {
                timeToSpawn -= 0;
            }

            if (timeToSpawn <= 0) //TimetoSpawn resets when <=0
            {
                timeToSpawn = originalTime;
                EventManager.SpawnChecker();
                //print("fn check");
            }

          

        }

        public void setTimeToSpawn(float f)
        {
            timeToSpawn = f;
            originalTime = timeToSpawn;
        }


        /*public void EWarning()
        {
            if (timeToSpawn <= 20)
            {
                warning.Play();
                print("help");
            }
        }*/
        
    }
}