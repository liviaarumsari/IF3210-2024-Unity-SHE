using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyAttackRanged : MonoBehaviour
    {
        public float timeBetweenShots = 0.5f;
        public int damagePerShot = 10;
        public int bulletsPerShot = 6;
        public float inaccuracyDistance = 5f;
        public float range = 30f;
        public GameObject laser;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealthSimple enemyHealth;
        BoxCollider boxCollider;
        bool playerInRange;
        float timer;

        Ray shootRay = new Ray();
        RaycastHit shootHit;
        int shootableMask;
        ParticleSystem gunParticles;
        AudioSource gunAudio;

        bool isSpawned = false;
        float effectsDisplayTime = 0.2f;

        void Awake ()
        {
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();

            shootableMask = LayerMask.GetMask("Shootable");
            boxCollider = GetComponent<BoxCollider> ();

            gunParticles = GetComponent<ParticleSystem>();
            gunAudio = GetComponent<AudioSource>();

            enemyHealth = GetComponentInParent<EnemyHealthSimple> ();
            anim = GetComponentInParent<Animator> ();

            CalculateBoxColliderSize();
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
            if (isSpawned && enemyHealth.CurrentHealth() > 0 && playerHealth.currentHealth > 0)
            {
            timer += Time.deltaTime;

                if (timer >= timeBetweenShots && playerInRange)
                {
                    Attack();
                    anim.SetTrigger("Attack");
                } 
            }
        }

        void Attack ()
        {
            timer = 0f;

            if (gunAudio != null)
            {
                gunAudio.Play();
            }

            gunParticles.Stop();
            gunParticles.Play();


            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            for (int i = 0; i < bulletsPerShot; i++)
            {
                Vector3 shootingDir = GetShootingDirection();
                if (Physics.Raycast(shootRay.origin, GetShootingDirection(), out shootHit, range, shootableMask))
                {
                    PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();

                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamageShot(damagePerShot, shootHit.point);
                    }

                    CreateLaser(shootHit.point);
                }
                else
                {
                    CreateLaser(shootRay.origin + shootingDir * range);
                }
            }
        }

        Vector3 GetShootingDirection()
        {
            Vector3 targetPos = transform.position + transform.forward * range;
            targetPos = new Vector3(targetPos.x + Random.Range(-inaccuracyDistance, inaccuracyDistance),
                targetPos.y,
                targetPos.z + Random.Range(-inaccuracyDistance, inaccuracyDistance));
            Vector3 direction = targetPos - transform.position;
            return direction.normalized;
        }

        void CalculateBoxColliderSize()
        {
            boxCollider.center = new Vector3(0, 0, range / 2);
            boxCollider.size = new Vector3(inaccuracyDistance, boxCollider.size.y, range);
        }

        void CreateLaser(Vector3 end)
        {
            if (laser != null)
            {
                Light light = Instantiate(laser).GetComponent<Light>();
                light.enabled = true;
                LineRenderer lr = Instantiate(laser).GetComponent<LineRenderer>();
                lr.enabled = true;
                lr.SetPositions(new Vector3[2] { transform.position, end });
                StartCoroutine(DestroyLaserAfterDelay(lr.gameObject, light.gameObject));
            }
        }

        IEnumerator DestroyLaserAfterDelay(GameObject lrGameObject, GameObject lightGameObject)
        {
            yield return new WaitForSeconds(timeBetweenShots * effectsDisplayTime);

            // Destroy the laser GameObjects
            Destroy(lrGameObject);
            Destroy(lightGameObject);
        }

        public void FinishSpawnAnimAttack()
        {
            isSpawned = true;
        }
    }
}