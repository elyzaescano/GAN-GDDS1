using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{
    public class ChaseState : State
    {
        public SearchState searchState;
        public GameObject enemyHolder;
        public float timeToChase = 10f;

        public float distanceFromTarget;
        public float stoppingDistance;

        public override State Tick(EnemyManager enemyManager, EnemyFieldOfView enemyFOV, EnemySpawnManager enemySpawn, EnemyAnimationHandler enemyAnim)
        {
            if (enemySpawn.dialogBox.activeInHierarchy) timeToChase -= 0;
            else timeToChase -= Time.deltaTime;
            
            if (enemyFOV.currentTarget != null)
            {
                //Resets the time to search for the Search State
                searchState.timeToSearch = 10.0f;

                //Literally just finds the distance from target 
                distanceFromTarget = Vector3.Distance(enemyFOV.currentTarget.transform.position, enemyManager.navAgent.transform.position);

                //Makes FOV rotate to player
                //enemyFOV.transform.right = enemyFOV.currentTarget.transform.position - enemyManager.transform.position;

                if (distanceFromTarget > stoppingDistance)
                {
                    //Makes enemy move to player
                    enemyManager.navAgent.SetDestination(enemyFOV.currentTarget.transform.position);
                }

            if (timeToChase <= 0)
            {
                EventManager.EnemyDied();
                Destroy(enemyHolder);
                EventManager.EnemyDeath += enemySpawn.DespawnEnemy;

            }

                return this;
            }
            else
            {
                //enemySpawn.canSpawn = true;
                return searchState;
            }

        }
    }
}

