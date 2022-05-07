using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class EnemySpawnManager : MonoBehaviour
    {
        // The enemy prefab to be spawned.
        public static EnemySpawnManager instance;
        public GameObject enemy;

        // An array of the spawn points this enemy can spawn from.
        public Transform[] spawnPoints;

        public float maxTime = 50;
        public float minTime = 10;

        //make sure only one enemy can be on the scene at all times
        //current time
        private float time;

        //The time to spawn the object
        private float spawnTime;

        void Awake()
        {
            List<Transform> spawningPointsAsList = new List<Transform>();

            // Get All the children of the object this script is assigned to (EnemyManager) and consider them as spawning points
            foreach (Transform child in transform)
            {
                spawningPointsAsList.Add(child);
            }

            spawnPoints = spawningPointsAsList.ToArray();

            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else if (instance != null)
            {
                Destroy(this);
            }
        }


        void Start()
        {
            SetRandomTime();
            time = 0;
        }

        //Sets the random time between minTime and maxTime
        void SetRandomTime()
        {
            spawnTime = Random.Range(minTime, maxTime);
            Debug.Log("Next object spawn in " + spawnTime + " seconds.");
        }

        void FixedUpdate()
        {
            //Counts up
            time += Time.deltaTime;
            //Check if its the right time to spawn the object
            if (time >= spawnTime)
            {
                Debug.Log("Time to spawn: " + enemy.name);
                Spawn();
                SetRandomTime();
                time = 0;
            }
        }

        void Spawn()
        {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
