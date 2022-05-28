using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Animation Handler for enemy needs to be added
namespace EnemyAI
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyFieldOfView enemyFOV;
        EnemySpawnManager enemySpawn;
        EnemyAnimationHandler enemyAnim;

        public State currentState;

        public State searchState;
        public GameObject target;
        public Vector3 playerPos;

        public NavMeshAgent navAgent; //Think of the NavMeshAgent as a rigidbody that can detect walls
        public Rigidbody2D enemyrb;
        bool hasSwitched = false;

        public float startWaitTime { get; private set; }
        public float waitTime;


        private void Awake()
        {
            enemyrb = GetComponent<Rigidbody2D>();
            enemyFOV = FindObjectOfType<EnemyFieldOfView>();
            navAgent = GetComponentInParent<NavMeshAgent>();
            navAgent.updateRotation = false;
            navAgent.updateUpAxis = false;
            navAgent.autoBraking = false;

            waitTime = startWaitTime;
        }

        void FixedUpdate()
        {
            HandleStateMachine();

            if (currentState == searchState)
            {
                if (!hasSwitched)
                {
                    playerPos = new Vector3(target.transform.position.x, target.transform.position.y, 0);
                    hasSwitched = true;
                }
            }
            else
            {
                hasSwitched = false;
            }
        }

        private void HandleStateMachine()
        {
            if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyFOV, enemySpawn, enemyAnim);

                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
            }
        }
    }
}
