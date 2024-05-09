using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class EnemyMovementRanged : MonoBehaviour
    {
        Transform player;
        Transform enemyTransform;
        PlayerHealth playerHealth;
        EnemyHealthSimple enemyHealth;
        NavMeshAgent nav;
        Animator anim;
        bool isSpawned = false;

        EnemyAoEWeaken enemyAoEWeaken;
        EnemyAttackRanged enemyAttack;

        void Awake ()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent<PlayerHealth> ();
            enemyTransform = GetComponent<Transform>();
            nav = GetComponent<NavMeshAgent>();
            enemyHealth = GetComponent<EnemyHealthSimple>();

            enemyAoEWeaken = GetComponent<EnemyAoEWeaken>();
            enemyAttack = GetComponent<EnemyAttackRanged>();

            if (enemyAoEWeaken == null)
                enemyAoEWeaken = GetComponentInChildren<EnemyAoEWeaken>();

            if (enemyAttack == null)
                enemyAttack = GetComponentInChildren<EnemyAttackRanged>();
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

            if (enemyAoEWeaken != null)
                enemyAoEWeaken.FinishSpawnAnimAoE();
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