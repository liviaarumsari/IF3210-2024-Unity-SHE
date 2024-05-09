using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class PetWolfAttack : MonoBehaviour
    {
        public int attackDamage = 10;
        public float timeBetweenAttacks = 0.5f;

        Animator anim;
        Transform targetEnemy;
        GameObject targetedEnemyObject;
        EnemyHealthSimple enemyHealth;
        PetWolfMovement movement;
        bool enemyInRange;
        float timer;

        void Awake()
        {
            movement = GetComponent<PetWolfMovement>();
            anim = GetComponent<Animator>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == targetedEnemyObject)
            {
                enemyInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == targetedEnemyObject)
            {
                enemyInRange = false;
            }
        }

        // Update is called once per frame
        void Update()
        {   
        
            targetedEnemyObject = movement.targetedEnemyObject;

            if(targetedEnemyObject != null)
            {
                targetEnemy = movement.targetEnemy;
                enemyHealth = targetedEnemyObject.GetComponent<EnemyHealthSimple>();

                timer += Time.deltaTime;

                if (timer >= timeBetweenAttacks && enemyInRange && enemyHealth.currentHealth > 0)
                {
                    anim.SetTrigger("Attack");
                    Attack();
                }
                else
                {
                    anim.SetTrigger("NotAttack");
                }

            }

            // if (enemyHealth.currentHealth <= 0)
            //{
              //  anim.SetTrigger("PlayerDead");
            //}
        }

        void Attack()
        {
            timer = 0f;
           
            if (enemyHealth.currentHealth > 0)
            {
                enemyHealth.TakeDamage(attackDamage, targetEnemy.position);
            }
           
        }
    }

}
