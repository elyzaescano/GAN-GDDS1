using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{
    public class PatrolState : State
    {
        public ChaseState chaseState;
        public SearchState searchState;

        public bool pathAvailable;

        [Header("Patrol Waypoints")]
        public Transform[] points;
        public int randomPoint;

        public override State Tick(EnemyManager enemyManager, EnemyFieldOfView enemyFOV, EnemySpawnManager enemySpawn)
        {
            //Enemy will pick a random point out of the designated waypoints to go to
            enemyManager.navAgent.SetDestination(points[randomPoint].transform.position);

            //Loads the next random point for the enemy to go to
            if (enemyManager.navAgent.remainingDistance < 0.1f)
            {
                if (enemyManager.waitTime <= 0) //I forgot why I did this. Laughing out loud
                {
                    //Enemy will pick a random point out of the designated waypoints to go to
                    randomPoint = Random.Range(0, points.Length);
                }
                else
                {
                    enemyManager.waitTime -= Time.deltaTime;
                }
            }

            //Resets the time to search for the Search State
            searchState.timeToSearch = 10.0f;

            #region Handle State Switching
            if (enemyFOV.currentTarget != null)
            {
                return chaseState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}
