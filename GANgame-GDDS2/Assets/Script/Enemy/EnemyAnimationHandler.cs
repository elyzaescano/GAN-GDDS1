using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class EnemyAnimationHandler : MonoBehaviour
    {
        Animator anim;

        EnemyFieldOfView enemyFOV;

        void Awake()
        {
            anim = GetComponent<Animator>();
            enemyFOV = FindObjectOfType<EnemyFieldOfView>();
        }

        void Update()
        {
            Vector3 dir;

            dir = enemyFOV.currentWaypoint.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();

            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);

        }
    }
}