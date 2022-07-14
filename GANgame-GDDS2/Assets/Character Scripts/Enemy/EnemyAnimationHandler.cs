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

            dir = enemyFOV.player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();

            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("RunTree"))
            {
                
                List<AnimatorClipInfo> clips = new List<AnimatorClipInfo>(anim.GetCurrentAnimatorClipInfo(0));
                clips.Sort(SortByWeight);

                if (clips[0].clip.name == "monster_walkdown")
                {
                    //Changes direction of FOV
                    enemyFOV.transform.eulerAngles = new Vector3(0, 0, 270); 
                }
                if (clips[0].clip.name == "monster_walkup")
                {
                    enemyFOV.transform.eulerAngles = new Vector3(0, 0, 90);
                }
                if (clips[0].clip.name == "monster_walkleft")
                {
                    enemyFOV.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                if (clips[0].clip.name == "monster_walkright")
                {
                    enemyFOV.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        }

        private int SortByWeight(AnimatorClipInfo x, AnimatorClipInfo y)
        {
            return x.weight > y.weight ? -1 : 1;
        }
    }
}