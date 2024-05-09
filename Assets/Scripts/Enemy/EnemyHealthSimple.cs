using UnityEngine;

namespace Nightmare
{
    public class EnemyHealthSimple : MonoBehaviour
    {
        GameManager gameManager;

        public int startingHealth = 100;
        public int currentHealth;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public AudioClip deathClip;

        private bool isSinking = false;

        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        SphereCollider sphereCollider;
        //EnemyMovementSimple enemyMovement;

        void Awake ()
        {
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
            sphereCollider = GetComponent<SphereCollider>();
            //enemyMovement = GetComponent<EnemyMovementSimple>();

            currentHealth = startingHealth;

            gameManager = GameManager.Instance;
        }

        void OnEnable()
        {
            currentHealth = startingHealth;
            SetKinematics(false);
        }

        private void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }

        void Update ()
        {
            if (IsDead() && isSinking)
            {
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -10f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        public bool IsDead()
        {
            return (currentHealth <= 0f);
        }

        public void TakeDamage (int amount, Vector3 hitPoint)
        {
            if (!IsDead())
            {
                enemyAudio.Play();
                currentHealth -= amount;

                if (IsDead())
                {
                    Death();
                }
                /*else
                {
                    enemyMovement.GoToPlayer();
                }*/
            }
                
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
        }

        void Death ()
        {
            //EventManager.TriggerEvent("Sound", this.transform.position);
            anim.SetTrigger ("Dead");

            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }

        void Sink()
        {
            isSinking = true;
            Destroy(gameObject, 2f);
        }

        public void StartSinking()
        {
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
            SetKinematics(true);
            capsuleCollider.enabled = false;
            if (sphereCollider != null)
                sphereCollider.enabled = false;

            gameManager.currentGameState.AddScore(scoreValue);
            Invoke("Sink", 1f);
        }

        public int CurrentHealth()
        {
            return currentHealth;
        }
    }
}