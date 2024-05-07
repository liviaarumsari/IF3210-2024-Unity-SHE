using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnEnemy : MonoBehaviour
{
    public float spawnRadius = 4f;
    public GameObject spawnableEnemy;
    public float startDelay = 3f;
    public float waitTime = 3f;
    public PlayerHealth playerHealth;
    Transform parentTransform;

    void Start()
    {
        parentTransform = GetComponent<Transform>();
        InvokeRepeating("Spawn", startDelay, waitTime);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f) return;

        Vector2 randomPointOnCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 randomPoint = parentTransform.position + new Vector3(randomPointOnCircle.x, 0, randomPointOnCircle.y);
        Debug.Log(randomPoint);
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, NavMesh.AllAreas)){
            Debug.Log(hit.position);
            Instantiate(spawnableEnemy, new Vector3(hit.position.x, parentTransform.position.y, hit.position.z), parentTransform.rotation);
        }
    }
}
