using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyFieldOfView enemyFOV;
        EnemySpawnManager enemySpawn;
        EnemyAnimationHandler enemyAnim;
        PauseScreen pause;
        public GameObject deathScreen;
        public GameObject enemyHolder;
        public Transform playerReset;
        public State currentState;

        public State searchState;
        public GameObject target {get ; private set;}
        public Vector3 playerPos;
        [SerializeField]float difference;
        public float deathreshold = 1;

        public NavMeshAgent navAgent; //Think of the NavMeshAgent as a rigidbody that can detect walls
        public Rigidbody2D enemyrb;
        bool hasSwitched = false;

        public float startWaitTime { get; private set; }
        public float waitTime;


        private void Awake()
        {
            enemyrb = GetComponent<Rigidbody2D>();
            enemyFOV = FindObjectOfType<EnemyFieldOfView>();
            enemyAnim = FindObjectOfType<EnemyAnimationHandler>();
            enemySpawn = FindObjectOfType<EnemySpawnManager>();
            target = GameObject.FindGameObjectWithTag("Player");
            navAgent = GetComponentInParent<NavMeshAgent>();
            navAgent.updateRotation = false;
            navAgent.updateUpAxis = false;
            navAgent.autoBraking = false;
            
            deathScreen = GameObject.FindGameObjectWithTag("LoseScreen").transform.GetChild(0).gameObject;
            playerReset = GameObject.FindGameObjectWithTag("Respawn").transform;

            waitTime = startWaitTime;

            pause = FindObjectOfType<PauseScreen>();

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

            if (enemyFOV.player.transform.position.y >= transform.position.y) GetComponent<SpriteRenderer>().sortingOrder = 2;
            else GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        private void Update() 
        {
            if (pause.isPaused)
            {
                navAgent.isStopped = true;
            }
            else
            {
                navAgent.isStopped = false;
            }

            difference = Vector3.Distance(transform.position, enemyFOV.player.transform.position);

            if (difference < deathreshold && enemyFOV.currentTarget != null)
            {
                print (difference);
                KillPlayer();
                AudioManager.instance.Stop("Monster");
                AudioManager.instance.Stop("Level");
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

        public void KillPlayer()
        {
            print("KillPlayerCode reached");
            deathScreen.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").transform.position = playerReset.position;
            enemySpawn.canSpawn = true;
            enemyHolder.gameObject.SetActive(false);
        }
    }
}
