using System.Collections;
using UnityEngine;

namespace Nightmare
{
    public class PetSlimeHeal : MonoBehaviour
    {
        public float timeBetweenHeals = 2.0f;
        public int healPoint = 10;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        bool playerInRange;
        float timer;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            anim = GetComponent<Animator>();
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

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenHeals && playerInRange && playerHealth.currentHealth < playerHealth.startingHealth)
            {
                Heal();
            }

            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }

        void Heal()
        {
            timer = 0f;
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeHeal(healPoint);
            }
        }
    }

}
