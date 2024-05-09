using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyAoEHealth : MonoBehaviour
    {
        public int weakenStep = 2;
        public int maxHealthWeakened = 20;

        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealthSimple enemyHealth;
        bool isSpawned = false;
        bool registered = false;
        bool playerInRange = false;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponentInParent<EnemyHealthSimple>();
        }

        void OnTriggerEnter (Collider other)
        {
            if(other.gameObject == player)
            {
                playerInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            if(other.gameObject == player)
            {
                playerInRange = false;
            }
        }

        void Update ()
        {
            if (!isSpawned) return;

            if (playerInRange && !registered && enemyHealth.CurrentHealth() > 0 && playerHealth.currentHealth > 0)
            {
                playerHealth.RegisterWeakenHealth(maxHealthWeakened, weakenStep);
                registered = true;
            }

            if (registered && (!playerInRange || enemyHealth.CurrentHealth() <= 0 || playerHealth.currentHealth <= 0))
            {
                playerHealth.UnregisterWeakenHealth(maxHealthWeakened, weakenStep);
                registered = false;
            }
        }

        public void FinishSpawnAnimAoE()
        {
            isSpawned = true;
        }
    }
}