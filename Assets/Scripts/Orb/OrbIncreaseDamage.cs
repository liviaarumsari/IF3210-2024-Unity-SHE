using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class OrbIncreaseDamage : MonoBehaviour
    {
        GameObject player;
        PlayerShooting playerShooting;
        float multiplier = 0.1f;
        bool playerInRange;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerShooting = player.GetComponent<PlayerShooting>();
        }
        void ApplyOrbEffect()
        {
            if ((playerShooting.damagePerShot / playerShooting.baseDamage) < 2.5)
            {
                playerShooting.damagePerShot += (multiplier * playerShooting.baseDamage);
            }
            else
            {
                // display maximum orb number excited
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (playerInRange)
            {
                ApplyOrbEffect();
            }
        }
    }

}