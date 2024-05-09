using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyAttackMelee : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealthSimple enemyHealth;
        bool playerInRange;
        float timer;
        bool isSpawned = false;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealthSimple>();
            anim = GetComponent <Animator> ();

            if (enemyHealth == null)
                enemyHealth = GetComponent<Transform>().parent.GetComponent<EnemyHealthSimple>();

            if (anim == null)
                anim = GetComponent<Transform>().parent.GetComponent<Animator>();
        }

        void OnTriggerEnter (Collider other)
        {
            // If the entering collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            // If the exiting collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }

        void Update ()
        {
            if (!isSpawned) return;
            
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.CurrentHealth() > 0 && playerHealth.currentHealth > 0)
            {
                // ... attack.
                Attack ();
                anim.SetTrigger("Attack");
            }

            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("Cheer");
            }
        }

        void Attack ()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerHealth.TakeDamage (attackDamage);
            }
        }

        public void FinishSpawnAnimAttack()
        {
            isSpawned = true;
        }
    }
}