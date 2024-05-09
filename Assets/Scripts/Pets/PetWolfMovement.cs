using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class PetWolfMovement : MonoBehaviour
    {
        UnityEngine.AI.NavMeshAgent nav;
        public Transform targetEnemy;
        public GameObject targetedEnemyObject = null;
        EnemyHealthSimple enemyHealth;

        void Start()
        {
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        void FindNearestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                enemyHealth = enemy.GetComponent<EnemyHealthSimple>(); 

                if (distanceToEnemy < shortestDistance && enemyHealth.currentHealth > 0)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
                targetedEnemyObject = nearestEnemy;
                targetEnemy = nearestEnemy.transform;
                MoveToTarget();
            }
        }

        void MoveToTarget()
        {
            if (targetEnemy != null)
            {
                nav.SetDestination(targetEnemy.position);
            }
        }

        void Update()
        {
            if (targetEnemy != null)
            {
                MoveToTarget();
            }
            else
            {
                FindNearestEnemy();
            }
        }

    }
}