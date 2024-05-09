using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private Animator anim;
    private Collider attackCollider;

    private float timer;
    private bool isAttacking = false;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponentInParent<Animator>();
        attackCollider = GetComponent<Collider>();
    }

    void Update()
    {
        // If the "Fire1" button is pressed and the player is not already attacking...
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            // Trigger the attack animation
            anim.SetTrigger("AttackSword");

            // Start the attack coroutine
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        // Set isAttacking to true to prevent multiple attacks at the same time
        isAttacking = true;

        // Wait for the animation to finish
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);

        // Enable the attack collider to detect enemies
        //attackCollider.enabled = true;

        // Wait for a short delay to allow enemies to be hit by the collider
        yield return new WaitForSeconds(0.1f);

        // Disable the attack collider
        //attackCollider.enabled = false;

        // Reset the timer for the next attack
        timer = 0f;

        // Set isAttacking to false to allow the player to attack again
        isAttacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy") && isAttacking)
        {
            // Attack the enemy
            Attack(other.gameObject);
        }
    }

    void Attack(GameObject enemy)
    {
        // Get the health component of the enemy
        EnemyHealthSimple enemyHealth = enemy.GetComponent<EnemyHealthSimple>();

        // Damage the enemy if it has health remaining
        if (enemyHealth != null && enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage, transform.position);
        }
    }
}
