// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Nightmare
// {
//     public class OrbIncreaseSpeed : MonoBehaviour
//     {
//         GameObject player;
//         PlayerMovement playerMovement;
//         bool playerInRange;
//         float multiplier = 0.2f;
//         public float orbEffectTime = 15f;
//         float timer;
//         bool orbTaken;

//         void Awake()
//         {
//             player = GameObject.FindGameObjectWithTag("Player");
//             playerMovement = player.GetComponent<PlayerMovement>();
//         }

//         void OnTriggerEnter(Collider other)
//         {
//             if (other.gameObject == player)
//             {
//                 playerInRange = true;
//             }
//         }

//         void ApplyOrbEffect()
//         {
//             playerMovement.speed += (multiplier * playerMovement.baseSpeed);
//             orbTaken = true;
//         }

//         void RemoveOrbEffect()
//         {
//             playerMovement.speed -= (multiplier * playerMovement.baseSpeed);
//         }

//         // Update is called once per frame
//         void Update()
//         {
//             if (playerInRange)
//             {
//                 ApplyOrbEffect();
//             }

//             if (orbTaken)
//             {
//                 timer += Time.deltaTime;
//             }

//             if (timer > orbEffectTime)
//             {
//                 RemoveOrbEffect();
//             }
//         }
//     }
// }

