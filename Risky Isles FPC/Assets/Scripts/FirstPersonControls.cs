
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class FirstPersonControls : MonoBehaviour
{
    private Controls playerInput;
    [Header("Movement Controls")]
    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    public Transform playerCamera; // Reference to the player's camera
    
    // Private variables to store input values and the character controller
    private Vector2 moveInput; // Stores the movement input from the player
    private Vector2 lookInput; // Stores the look input from the player
    private float verticalLookRotation = 0f; // Keeps track of vertical camera rotation for clamping
    private Vector3 velocity; // Velocity of the player
    private CharacterController characterController; // Reference to the CharacterController component

    [Header("Shooting Controls")] 
    public GameObject projectilePrefab;//Shooting Projectile Prefab
    public Transform firePoint; // Point in which the projectile is fired from
    public float projectileSpeed = 20f; // Speed at which the projectile is fired

    [Header("Picking Up Controls")] 
    public Transform holdPosition; // Position where the picked-up object will be held
    private GameObject heldObject; // Ref to the currently held object
    public float pickUpRange = 3f; // Range wherein objects can be picked up 
    private bool holdingGun = false;
    public TextMeshProUGUI interactableChecker;
    //public string nameStorage;
    public LayerMask interactableLayer;

    [Header("Note UI")] 
    public GameObject noteDisplayPanel;
    public TextMeshProUGUI noteTextDisplay;
    

    [Header("Crouch Controls")]
    public float crouchHeight = 1f; // make shorter 
    public float standingHeight = 2f; // make normal height
    public float crouchSpeed = 1.5f; // rate of movement while crouching 
    private bool isCrouching = false; // ensures default position is = standing

    [Header("Player HP:")] 
    public int playerHp;
    
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
        noteDisplayPanel.SetActive(false);
    }
    private void OnEnable()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
        // Create a new instance of the input actions
        playerInput = new Controls(); //var
        // Enable the input actions
        playerInput.Player.Enable();
        // Subscribe to the movement input events 
        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); //Update moveInput when movement input is performed
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // \ Reset moveInput when movement input is canceled 
       
        // Subscribe to the look input events 
        playerInput.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>(); //Update lookInput when look input is performed 
        playerInput.Player.LookAround.canceled += ctx => lookInput = Vector2.zero; // Reset lookInput when look input is canceled 
        // Subscribe to the jump input event 
        playerInput.Player.Jump1.performed += ctx => Jump(); //Call the Jump method when jump input is performed 
        // Subscribe to the shoot input event 
        playerInput.Player.Shooting.performed += ctx => Shoot(); // Call the Shoot method when shoot input is performed 
        // Subscribe to the pick-up input event 
        playerInput.Player.PickUp.performed += ctx => PickUpObject(); //Call the PickUpObject when pick-up input is performed
        playerInput.Player.Crouch.performed += ctx => ToggleCrouch(); // Call tbe ToggleCrouch Method when crouch input is performed
    }
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation
        Move();
        LookAround();
        ApplyGravity();
        CheckInteractability();
    }
    public void Move()
    {
        // Create a movement vector based on the input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        
        // Transform direction from local to world space
        move = transform.TransformDirection(move);
        
        //Adjust Speed if Crouching
        float currentSpeed;
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
        
        // Move the character controller based on the movement vector and speed
        characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    public void ToggleCrouch()
    {
        if (isCrouching)
        {
            //Stand Up 
            characterController.height = standingHeight;
            isCrouching = false;
        }
        else
        {
            //Crouch Down
            characterController.height = crouchHeight;
            isCrouching = true;
        }
    }
    public void LookAround()
    {
        // Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;
        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);
        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }
    public void ApplyGravity() 
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }
        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity
        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character
    }
    public void Jump()
    { 
        if (characterController.isGrounded)
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void Shoot()
    {
        if (holdingGun == true)
        {
            //Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            //Get the RigidBody component of the projectile  & set its velocity
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileSpeed;
            //Destroy the projectile after 3 seconds 
            Destroy(projectile, 3f);
        }
    }

    public void PickUpObject()
    {
        //Check if we're already holding an object 
        if (heldObject != null)   
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; //Enables Physics 
            heldObject.transform.parent = null;
            holdingGun = false;
            heldObject = null;
            return;
        }
        
        //Perfrom a Raycast from the camera's postion forward 
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
           
            //Check if the hit object has the tag "pickup"
            if (hit.collider.CompareTag("PickUp"))
            {
                //Pick up the object 
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                //Disable physics 
                // Attach the object to the hold position 
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
            }
            else if (hit.collider.CompareTag("Gun"))
            {
                //Pick up the Object 
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                //Disable Physics
                //Attach object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
                holdingGun = true;
            }
            else if (hit.collider.CompareTag("Drink"))
            {
                ConsumableItem consumable = hit.collider.GetComponent<ConsumableItem>();
                if (consumable != null)
                {
                    consumable.Consume(GetComponent<PlayerStats>());
                }
            }
            else if (hit.collider.CompareTag("Food"))
            {
                ConsumableItem consumable = hit.collider.GetComponent<ConsumableItem>();
                if (consumable != null)
                {
                    consumable.Consume(GetComponent<PlayerStats>());
                }
            }
            else if (hit.collider.CompareTag("Readable"))
            {
                StartCoroutine(ShowNoteUI());
                var message = hit.collider.GetComponent<Note>();
                noteTextDisplay.text = message.noteMessage;
            }
        }
    }

    private IEnumerator ShowNoteUI()
    {
        noteDisplayPanel.SetActive(true);        
        yield return new WaitForSeconds(2f);
        noteTextDisplay.text = " ";
        noteDisplayPanel.SetActive(false);
        yield return null;
    }

    public void HideNoteUI()
    {
        noteDisplayPanel.SetActive(false);
    }
    //Check Interactability shoots ray at object to check its tag/layer(details)
    public void CheckInteractability()
    {
        interactableChecker.text = " ";
        //Crates a beam that's labelled ray, starting from player camera position & it shoots in the direction the player is looking 
        Ray ray = new Ray(playerCamera.position, playerCamera.forward); 
        RaycastHit hit; //Result of what the ray hits 

        if (Physics.Raycast(ray, out hit, pickUpRange, interactableLayer))
        {
            interactableChecker.text = hit.collider.name;
        }
    }
}
