using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class EnemyFieldOfView : MonoBehaviour
    {
        public GameObject currentTarget;
        public GameObject currentWaypoint;
        public LayerMask targetLayer;
        public LayerMask obstructionLayer;

        //public PatrolState patrolState;
        public bool canSeePlayer { get; private set; }

        [Range(1, 360)] public float angle = 45f;
        public float detectionRadius;

        private void Awake()
        {
            //patrolState = FindObjectOfType<PatrolState>();

            StartCoroutine(FOVCheck());
            //Checks if the player is in 5 times per second as opposed to every frame
        }

        protected void LateUpdate()
        {
            transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
        }

        public IEnumerator FOVCheck()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                if (canSeePlayer)
                {
                    currentTarget = GameObject.FindGameObjectWithTag("Player");
                    currentWaypoint = GameObject.FindGameObjectWithTag("Player");
                }
                else
                {
                    currentTarget = null;
                    //currentWaypoint = patrolState.points[patrolState.randomPoint];
                }

                yield return wait;
                FOV();
            }
        }

        private void FOV()
        {
            #region Handle Enemy Target Detection
            Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetLayer);

            if (rangeCheck.Length > 0)
            {
                Transform target = rangeCheck[0].transform;
                Vector2 targetDirection = (target.position - transform.position).normalized;

                if (Vector2.Angle(transform.right, targetDirection) < angle / 2)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    //Checks if the raycast can reach the player WITHOUT hitting the obstruction layer
                    if (!Physics2D.Raycast(transform.position, targetDirection, distanceToTarget, obstructionLayer))
                    {
                        canSeePlayer = true;
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
            }
            #endregion
        }

        /*
        #region Visualising the FOV
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, detectionRadius);

            Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2 + 90);
            Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2 + 90);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + angle01 * detectionRadius);
            Gizmos.DrawLine(transform.position, transform.position + angle02 * detectionRadius);
            if (canSeePlayer)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, currentTarget.transform.position);
            }
            else
                return;
        }
        private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
        #endregion
        */
    }
}
