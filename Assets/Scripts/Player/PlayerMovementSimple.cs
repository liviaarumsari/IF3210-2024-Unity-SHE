using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerMovementSimple : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.
    public bool inShopArea = false;
    public bool inQuestArea = false;
    public bool inSaveArea = false;


    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    [SerializeField] private BannerMsg shopMsg;
    [SerializeField] private ShopUI shopUi;
    [SerializeField] private SaveUI saveUI;

    Vector3 lastPosition;
    GameManager gameManager;

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        lastPosition = transform.position;
    }

    void Update()
    {
        float distanceTravelled = Vector3.Distance(transform.position, lastPosition);
        gameManager.currentGameState.AddDistanceTravelled(distanceTravelled);
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Store the input axes.
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        Animating(h, v);

        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleShopEvent();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            HandleQuestStart();
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            HandleSave();
        }

        // TODO: Remove if real save game implemented
        if (Input.GetKeyDown(KeyCode.T))
        {
            gameManager.SaveCurrentGame(1);
        }

        // Cheat: Auto advance to next stage
        if (Input.GetKeyDown(KeyCode.N))
        {
            gameManager.currentGameState.AdvanceToNextStage();
        }

        // Cheat: Auto victory
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.currentGameState.EndGame(GameState.Stage.Victory);
        }

        // Cheat: Auto game over
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameManager.currentGameState.EndGame(GameState.Stage.GameOver);
        }
        }


        void Move(float h, float v)
        {
            // Set the movement vector based on the axis input.
            movement.Set(h, 0f, v);

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition(transform.position + movement);
        }


        void Turning()
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
        }


        void Animating(float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool("IsWalking", walking);
        }

        private IEnumerator ShowShopNotInRange(float duration)
        {
            shopMsg.Show();
            yield return new WaitForSeconds(duration);
            shopMsg.Hide();
        }

        private void HandleShopEvent()
        {
            IShopCustomer customer = this.gameObject.GetComponent<IShopCustomer>();
            if (inShopArea && customer != null)
            {
                shopUi.Show(customer);
            }
            else if (customer != null)
            {
                StartCoroutine(ShowShopNotInRange(1.5f));
            }
        }

        private void HandleQuestStart()
        {
            if (inQuestArea)
            {
                gameManager.currentGameState.AdvanceToNextStage();
            }
        }

        private void HandleSave()
        {
            if (inSaveArea)
            {
                saveUI.Show();
            }
        }
    }