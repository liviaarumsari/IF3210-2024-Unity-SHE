using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyAoEWeaken : MonoBehaviour
    {
        [Header("Health")]
        public int weakenHealthStep = 2;
        public int maxHealthWeakened = 20;

        [Header("Movement")]
        public float speedWeakenStep = 0.5f;
        public float maxSpeedWeakened = 2;

        [Header("Attack")]
        public int weakenAttackStep = 1;
        public int maxAttackWeakened = 5;

        GameObject player;
        PlayerHealth playerHealth;
        PlayerMovementSimple playerMovement;
        PlayerShooting playerShooting;

        EnemyHealthSimple enemyHealth;
        bool isSpawned = false;
        bool registered = false;
        bool playerInRange = false;

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            playerMovement = player.GetComponent<PlayerMovementSimple>();
            playerShooting = player.GetComponentInChildren<PlayerShooting>();
            enemyHealth = GetComponentInParent<EnemyHealthSimple>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
        }

        void Update()
        {
            if (!isSpawned) return;

            if (playerInRange && !registered && enemyHealth.CurrentHealth() > 0 && playerHealth.currentHealth > 0)
            {
                playerHealth.RegisterWeakenHealth(maxHealthWeakened, weakenHealthStep);
                playerMovement.RegisterWeakenSpeed(maxSpeedWeakened, speedWeakenStep);
                playerShooting.RegisterWeakenDamage(maxAttackWeakened, weakenAttackStep);

                registered = true;
            }

            if (registered && (!playerInRange || enemyHealth.CurrentHealth() <= 0 || playerHealth.currentHealth <= 0))
            {
                playerHealth.UnregisterWeakenHealth(maxHealthWeakened, weakenHealthStep);
                playerMovement.UnregisterWeakenSpeed(maxSpeedWeakened, speedWeakenStep);
                playerShooting.UnregisterWeakenDamage(maxAttackWeakened, weakenAttackStep);

                registered = false;
            }
        }

        public void FinishSpawnAnimAoE()
        {
            isSpawned = true;
        }
    }
}