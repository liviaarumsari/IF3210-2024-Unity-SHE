using UnityEngine;
using UnityEngine.Events;
using System.Text;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;
using Unity.VisualScripting;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject
    {
        [Header("General Stats")]
        [SerializeField] public int damagePerShot = 20;
        [SerializeField] public float timeBetweenBullets = 0.15f;
        [SerializeField] public float range = 100f;

        [SerializeField] float inaccuracyDistance = 5f;

        float timer;
        Ray shootRay = new Ray();
        RaycastHit shootHit;
        int shootableMask;
        ParticleSystem gunParticles;
        AudioSource gunAudio;

        float effectsDisplayTime = 0.2f;
  
        private UnityAction listener;
        private Animator playerAnimator;

        [Header("Shotgun")]
        [SerializeField] bool shotgun = false;
        [SerializeField] int bulletsPerShot = 6;

        [Header("Sword")]
        [SerializeField] bool sword = false;

        [Header("Laser")]
        [SerializeField] GameObject laser;

        void Awake ()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask ("Shootable");

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem> ();
            gunAudio = GetComponent<AudioSource> ();

            playerAnimator = GetComponentInParent<Animator>();

            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void Update ()
        {
            //if (isPaused)
            //    return;

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

#if !MOBILE_INPUT
            if (timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                if (Input.GetButton("Fire1"))
                {
                    // ... shoot the gun.
                    Shoot();
                }
            }
            
#else
            // If there is input on the shoot direction stick and it's time to fire...
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
                // ... shoot the gun
                Shoot();
            }
#endif
        }


        void Shoot ()
        {
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            if (gunAudio != null)
            {
                gunAudio.Play ();
            }

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop ();
            gunParticles.Play ();


            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (shotgun)
            {
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    Vector3 shootingDir = GetShootingDirection();
                    if (Physics.Raycast(shootRay.origin, GetShootingDirection(), out shootHit, range, shootableMask))
                    {
                        // Try and find an EnemyHealth script on the gameobject hit.
                        EnemyHealthSimple enemyHealth = shootHit.collider.GetComponent<EnemyHealthSimple>();

                        // If the EnemyHealth component exist...
                        if (enemyHealth != null)
                        {
                            // ... the enemy should take damage.
                            enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                        }

                        // Set the second position of the line renderer to the point the raycast hit.
                        CreateLaser(shootHit.point);
                    }
                    // If the raycast didn't hit anything on the shootable layer...
                    else
                    {
                        // ... set the second position of the line renderer to the fullest extent of the gun's range.
                        //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                        CreateLaser(shootRay.origin + shootingDir * range);
                    }
                }
            }
            else if(sword) {
                Vector3 shootingDir = GetShootingDirection();
                // Perform the raycast against gameobjects on the shootable layer and if it hits something...
                if (Physics.Raycast(shootRay.origin, shootingDir, out shootHit, range, shootableMask))
                {
                    // Try and find an EnemyHealth script on the gameobject hit.
                    EnemyHealthSimple enemyHealth = shootHit.collider.GetComponent<EnemyHealthSimple>();

                    // If the EnemyHealth component exist...
                    if (enemyHealth != null)
                    {
                        // ... the enemy should take damage.
                        enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                    }
                }
            }
            else
            {
                Vector3 shootingDir = GetShootingDirection();
                // Perform the raycast against gameobjects on the shootable layer and if it hits something...
                if (Physics.Raycast(shootRay.origin, shootingDir, out shootHit, range, shootableMask))
                {
                    // Try and find an EnemyHealth script on the gameobject hit.
                    EnemyHealthSimple enemyHealth = shootHit.collider.GetComponent<EnemyHealthSimple>();

                    // If the EnemyHealth component exist...
                    if (enemyHealth != null)
                    {
                        // ... the enemy should take damage.
                        enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                    }

                    // Set the second position of the line renderer to the point the raycast hit.
                    CreateLaser(shootHit.point);
                }
                // If the raycast didn't hit anything on the shootable layer...
                else
                {
                    // ... set the second position of the line renderer to the fullest extent of the gun's range.
                    CreateLaser(shootRay.origin + shootingDir * range);
                }
            }
        }

        Vector3 GetShootingDirection()
        {
            Vector3 targetPos = transform.position + transform.forward * range;
            targetPos = new Vector3(targetPos.x + Random.Range(-inaccuracyDistance, inaccuracyDistance),
                targetPos.y + Random.Range(-inaccuracyDistance, inaccuracyDistance),
                targetPos.z + Random.Range(-inaccuracyDistance, inaccuracyDistance));
            Vector3 direction = targetPos - transform.position;
            return direction.normalized;
        }

        void CreateLaser(Vector3 end)
        {
            Light light = Instantiate(laser).GetComponent<Light>();
            light.enabled = true;
            LineRenderer lr = Instantiate(laser).GetComponent<LineRenderer>();
            lr.enabled = true;
            lr.SetPositions(new Vector3[2] {transform.position, end});
            StartCoroutine(DestroyLaserAfterDelay(lr.gameObject, light.gameObject));
        }

        IEnumerator DestroyLaserAfterDelay(GameObject lrGameObject, GameObject lightGameObject)
        {
            yield return new WaitForSeconds(timeBetweenBullets * effectsDisplayTime);

            // Destroy the laser GameObjects
            Destroy(lrGameObject);
            Destroy(lightGameObject);
        }
    }
}