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
        public float timeToSpawn; //the countdown timer
        float originalTime; //the static initial time set for spawn
        public Transform spawnPoint;
        PauseScreen pause;
        EnemySpawnManager esm;
        public AudioSource warning;
        bool isWarned = false;
               
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
                timeToSpawn -= Time.deltaTime; //timetospawn = ~ - time.deltatime //then count down from timer
            }
            else
            {
                timeToSpawn -= 0; //else please do not count down please please pllease
            }

            if (timeToSpawn <= 0) //TimetoSpawn resets when <=0
            {
                timeToSpawn = originalTime;
                EventManager.SpawnChecker();
                //print("fn check");
            }

            EWarning();

        }

        public void setTimeToSpawn(float f)
        {
            timeToSpawn = f;
            //timeToSpawn = originalTime;
        }


        public void EWarning()
        {
            if (!isWarned && timeToSpawn <=20)
            {
                warning.Play();
                print("help");
                isWarned = true;
            }
        }
        
    }
}