using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class EnemyMovementSimple : MonoBehaviour
    {
        Transform player;
        PlayerHealth playerHealth;
        EnemyHealthSimple enemyHealth;
        NavMeshAgent nav;
        Animator anim;

        void Awake ()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent<PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealthSimple> ();
            nav = GetComponent<NavMeshAgent>();
        }

        void Update ()
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                nav.SetDestination(player.position);
                anim.SetBool("IsMoving", true);
            } else
            {
                nav.enabled = false;
            }
        }
    }
}