using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class EnemyMovementSimple : MonoBehaviour
    {
        Transform player;
        // PlayerHealth playerHealth;
        // EnemyHealth enemyHealth;
        NavMeshAgent nav;

        void Awake ()
        {
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            nav = GetComponent<NavMeshAgent>();
        }

        void Update ()
        {
            nav.SetDestination(player.position);
        }
    }
}