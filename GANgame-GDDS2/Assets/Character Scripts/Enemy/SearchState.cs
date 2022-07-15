using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class SearchState : State
    {
        public GameObject enemyHolder;
        public ChaseState chaseState;
        public PatrolState patrolState;
        [Range(1, 200)] public float rotationSpeed;
        public float timeToSearch = 10f;

        public override State Tick(EnemyManager enemyManager, EnemyFieldOfView enemyFOV, EnemySpawnManager enemySpawn, EnemyAnimationHandler enemyAnim)
        {
            //Enemy goes to last known player position
            enemyManager.navAgent.SetDestination(enemyManager.playerPos);

            //Rotates on the spot of last known player position to look for the player
            float distanceFromTarget = Vector3.Distance(enemyFOV.player.transform.position, enemyManager.navAgent.transform.position);

            if (distanceFromTarget < 0.5f)
            {
                enemyAnim.StartCoroutine(enemyAnim.SearchSpin());
            }
            if (timeToSearch <= 0)
            {
                EventManager.EnemyDied();
                Destroy(enemyHolder);
            }

            //Counts down from the moment enemy switches to search state
            timeToSearch -= Time.deltaTime;

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
