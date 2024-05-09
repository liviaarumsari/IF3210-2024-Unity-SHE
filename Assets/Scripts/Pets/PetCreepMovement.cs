using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCreepMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    Transform player;
    // Transform owner;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is within detection range
        if (IsPlayerDetected())
        {
            // Calculate direction away from the player
            Vector3 awayFromPlayer = transform.position - player.position;

            // Calculate a new destination to avoid the player
            Vector3 newDestination = transform.position + awayFromPlayer.normalized * 5f;

            // Set the AI's destination to the new position
            nav.SetDestination(newDestination);
        }
        else
        {
            // If player is not detected, move towards a random destination
            Vector3 randomDestination = Random.insideUnitSphere * 10f;
            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(randomDestination, out hit, 10f, UnityEngine.AI.NavMesh.AllAreas);
            nav.SetDestination(hit.position);
        }
    }

    bool IsPlayerDetected()
    {
        float detectionRadius = 10f;
        return Vector3.Distance(transform.position, player.position) < detectionRadius;
    }
}
