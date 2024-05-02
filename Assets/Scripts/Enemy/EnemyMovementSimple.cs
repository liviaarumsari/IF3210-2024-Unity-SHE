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

        void Awake ()
        {
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
            } else
            {
                nav.enabled = false;
            }
        }
    }
}