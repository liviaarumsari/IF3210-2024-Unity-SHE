using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class EnemyMovementSimple : MonoBehaviour
    {
        Transform player;
        Transform enemyTransform;
        PlayerHealth playerHealth;
        EnemyHealthSimple enemyHealth;
        EnemyAttackMelee enemyAttack;
        NavMeshAgent nav;
        Animator anim;
        bool isSpawned = false;

        void Awake ()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent<PlayerHealth> ();
            enemyTransform = GetComponent<Transform>();
            enemyAttack = GetComponent<EnemyAttackMelee>();
            enemyHealth = GetComponent<EnemyHealthSimple> ();
            nav = GetComponent<NavMeshAgent>();
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
            enemyAttack.FinishSpawnAnimAttack();
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