using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class EnemyMovementMelee : MonoBehaviour
    {
        Transform player;
        Transform enemyTransform;
        PlayerHealth playerHealth;
        EnemyHealthSimple enemyHealth;
        NavMeshAgent nav;
        Animator anim;
        bool isSpawned = false;

        EnemyAoEHealth enemyAoE;
        EnemyAttackMelee enemyAttack;

        void Awake ()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent<PlayerHealth> ();
            enemyTransform = GetComponent<Transform>();
            nav = GetComponent<NavMeshAgent>();
            enemyHealth = GetComponent<EnemyHealthSimple> ();

            enemyAoE= GetComponent<EnemyAoEHealth>();
            enemyAttack = GetComponent<EnemyAttackMelee>();

            if (enemyAoE == null)
                enemyAoE = GetComponentInChildren<EnemyAoEHealth>();

            if (enemyAttack == null)
                enemyAttack = GetComponentInChildren<EnemyAttackMelee>();
            
        }

        void Update ()
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && isSpawned)
            {
                nav.SetDestination(player.position);
                anim.SetBool("IsMoving", true);
            } else
            {
                nav.enabled = false;
            }
        }

        public void FinishSpawnAnimMovement()
        {
            isSpawned = true;
            nav.enabled = true;

            if (enemyAttack != null)
                enemyAttack.FinishSpawnAnimAttack();

            if (enemyAoE != null)
                enemyAoE.FinishSpawnAnimAoE();
        }

        private void OnDisable()
        {
            nav.enabled = false;
        }

        private void OnEnable()
        {
            nav.enabled = true;
        }
    }
}