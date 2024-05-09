using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nightmare
{
    public class PlayerHealth : MonoBehaviour
    {
        GameManager gameManager;

        public int startingHealth = 100;
        public int currentHealth;
        int unweakenedHealth;

        public Slider healthSlider;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
        public bool godMode = false;

        Animator anim;
        AudioSource playerAudio;
        PlayerMovementSimple playerMovement;
        PlayerShooting playerShooting;
        bool isDead;
        bool damaged;

        public float timeBetweenWeaken = 0.5f;
        public int healthRegenStep = 2;

        int maxHealthWeakened = 0;
        List<int> maxHealthWeakenedList = new List<int>();
        int healthWeakenStep = 0;
        float weakenTimer;
        bool isWeakenOrRegen = false;

        void Awake()
        {
            // Setting up the references.
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovementSimple>();
            playerShooting = GetComponentInChildren<PlayerShooting>();
            weakenTimer = timeBetweenWeaken;

            gameManager = GameManager.Instance;

            ResetPlayer();
        }

        public void ResetPlayer()
        {
            // Set the initial health of the player.
            currentHealth = startingHealth;
            unweakenedHealth = currentHealth;

            playerMovement.enabled = true;
            //playerShooting.enabled = true;

            anim.SetBool("IsDead", false);
        }


        void Update()
        {
            if (isWeakenOrRegen)
                weakenTimer += Time.deltaTime;

            if (weakenTimer >= timeBetweenWeaken && Time.timeScale != 0)
            {
                if (isWeakenOrRegen)
                    weakenTimer = 0;

                if (healthWeakenStep > 0)
                {
                    Weaken();
                }
                else
                {
                    Unweaken();
                }

                if (isWeakenOrRegen && unweakenedHealth == currentHealth)
                {
                    isWeakenOrRegen = false;
                    weakenTimer = timeBetweenWeaken;
                }
            }

            if (damaged)
            {
                damageImage.color = flashColour;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            damaged = false;
        }

        void Weaken()
        {
            int amount;
            if (currentHealth - healthWeakenStep < unweakenedHealth - maxHealthWeakened)
            {
                amount = currentHealth - (unweakenedHealth - maxHealthWeakened);
            }
            else
            {
                amount = healthWeakenStep;
            }

            isWeakenOrRegen = true;
            ReduceHealth(amount);
        }

        void Unweaken()
        {
            if (currentHealth == unweakenedHealth) return;

            int amount;
            if (currentHealth + healthRegenStep > unweakenedHealth)
            {
                amount = unweakenedHealth - currentHealth;
            }
            else
            {
                amount = healthRegenStep;
            }

            isWeakenOrRegen = true;
            IncreaseHealth(amount);
        }

        void ReduceHealth(int amount)
        {
            damaged = true;

            currentHealth -= amount;
            healthSlider.value = currentHealth;

            playerAudio.Play();

            gameManager.currentGameState.health = currentHealth;
            gameManager.currentGameState.AddDamageReceived(amount);

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }

        void IncreaseHealth(int amount)
        {
            if (isDead) return;

            currentHealth += amount;
            healthSlider.value = currentHealth;

            gameManager.currentGameState.health = currentHealth;
        }

        public void TakeDamage(int amount)
        {
            if (godMode)
                return;

            ReduceHealth(amount);
            unweakenedHealth -= amount;
        }

        void Death()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;

            // Turn off any remaining shooting effects.
            //playerShooting.DisableEffects();

            // Tell the animator that the player is dead.
            anim.SetBool("IsDead", true);

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.clip = deathClip;
            playerAudio.Play();

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            playerShooting.enabled = false;

            gameManager.currentGameState.EndGame(GameState.Stage.GameOver);
        }

        public void RestartLevel()
        {
            //EventManager.TriggerEvent("GameOver");
        }

        public void RegisterWeakenHealth(int maxHealthWeakened, int healthWeakenStep)
        {
            if (godMode)
                return;

            this.maxHealthWeakened = maxHealthWeakened > this.maxHealthWeakened ? maxHealthWeakened : this.maxHealthWeakened;
            this.healthWeakenStep += healthWeakenStep;
            maxHealthWeakenedList.Add(maxHealthWeakened);
        }

        public void UnregisterWeakenHealth(int maxHealthWeakened, int healthWeakenStep)
        {
            this.healthWeakenStep -= healthWeakenStep;

            maxHealthWeakenedList.Remove(maxHealthWeakened);

            this.maxHealthWeakened = maxHealthWeakenedList.Count > 0 ? maxHealthWeakenedList.Max() : 0;
        }
    }
}