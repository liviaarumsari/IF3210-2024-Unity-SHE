using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class OrbHeal : MonoBehaviour
    {
        GameObject Player;
        float multiplier = 0.2f;
        PlayerHealth playerHealth;
        bool playerInRange;

        void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = Player.GetComponent<PlayerHealth>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }

        void ApplyOrbEffect()
        {
            playerHealth.currentHealth += (multiplier * playerHealth.startingHealth);

            if (playerHealth.currentHealth > playerHealth.startingHealth)
            {
                playerHealth.currentHealth = playerHealth.startingHealth;
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}