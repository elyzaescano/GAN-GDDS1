using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EnemyManager enemyManager, EnemyFieldOfView enemyFOV, EnemySpawnManager enemySpawn, EnemyAnimationHandler enemyAnim);
    }
}