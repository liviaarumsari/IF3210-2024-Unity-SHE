using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySummon : MonoBehaviour
{
    public float spawnRadius = 4f;
    public GameObject spawnableEnemy;
    public float startDelay = 3f;
    public float waitTime = 3f;

    PlayerHealth playerHealth;
    GameObject player;
    Transform parentTransform;
    Animator anim;
    EnemyMovementSimple enemyMovement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        parentTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovementSimple>();

        InvokeRepeating("StartSummonAnim", startDelay, waitTime);
    }

    void StartSummonAnim()
    {
        if (playerHealth.currentHealth <= 0f) return;

        enemyMovement.enabled = false;
        anim.SetTrigger("Summon");
    }

    public void Summon()
    {
        Vector2 randomPointOnCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 randomPoint = parentTransform.position + new Vector3(randomPointOnCircle.x, 0, randomPointOnCircle.y);
        Debug.Log(randomPoint);
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, NavMesh.AllAreas))
        {
            Debug.Log(hit.position);
            Instantiate(spawnableEnemy, new Vector3(hit.position.x, parentTransform.position.y, hit.position.z), parentTransform.rotation);
        }
    }

    public void FinishSummonAnim()
    {
        enemyMovement.enabled = true;
    }
}
