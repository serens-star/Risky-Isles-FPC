
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    private bool isRotatingObject = false;
    public float rotationSpeed = 100f;
    //private bool holdingGun = false;
    public TextMeshProUGUI interactableChecker;
    //public string nameStorage;
    public LayerMask interactableLayer;

    [Header("Rotation Controls")] 
    //public float rotationSpeed = 100f; //The speed the objects will rotate in
    private Vector2 rotateInput; //Stores rotation input from player
    //private bool isRotatingObject = false;

    [Header("Suspension Settings")] 
    public float holdDistance = 3f;

    [Header("Note UI")] 
    public GameObject noteDisplayPanel;
    public TextMeshProUGUI noteTextDisplay;
    

    [Header("Crouch Controls")]
    public float crouchHeight = 1f; // make shorter 
    public float standingHeight = 2f; // make normal height
    public float crouchSpeed = 1.5f; // rate of movement while crouching 
    private bool isCrouching = false; // ensures default position is = standing

    [Header("Pickup UI")] 
    public GameObject pickupImagePopup;
    public TextMeshProUGUI pickupText;

    [Header("Pickup Notification")] 
    public TextMeshProUGUI pickupNotificationText;
    public float notificationDuration = 2f;
    private Coroutine notificationCoroutine;
    
    
    
    

    private Light heldObjectLight;

    /*[Header("Player HP:")] 
    public int playerHp;*/
    
    
    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
        noteDisplayPanel.SetActive(false);
    }*/
    private void OnEnable()
    {
        characterController = GetComponent<CharacterController>();
        
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
        //playerInput.Player.Shooting.performed += ctx => Shoot(); // Call the Shoot method when shoot input is performed 
        // Subscribe to the pick-up input event 
        playerInput.Player.PickUp.performed += ctx => PickUpOrDropObject(); //Call the PickUpObject when pick-up input is performed
        playerInput.Player.Crouch.performed += ctx => ToggleCrouch(); // Call tbe ToggleCrouch Method when crouch input is performed
        
        //Interact with object using the F Key
        playerInput.Player.InterAct.performed += ctx => Interact(); //Call the PickUpObject when pick-up input is performed
        
        //Rotation Input
        playerInput.Player.RotateObject.performed += ctx => isRotatingObject = true;
        playerInput.Player.RotateObject.canceled += ctx => isRotatingObject = false;
    }
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation
        if (!isRotatingObject)
        {
            Move();
            LookAround();
        }
        
        ApplyGravity();
        CheckInteractability();
        
        if (heldObject != null)
        {
            PositionHeldObject();
        }

        if (isRotatingObject && heldObject != null)
        {
            RotateHeldObject();
        }
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

    /*public void Shoot()
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
    }*/

    public void PickUpOrDropObject()
    {
        //Check if we're already holding an object 
        if (heldObject != null)   
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; //Enables Physics 
            heldObject.transform.parent = null;
            

            if (heldObjectLight != null)
            {
                heldObjectLight.enabled = true;
            }

            PickupAudio pickupAudio = heldObject.GetComponent<PickupAudio>();
            if (pickupAudio != null && pickupAudio.audioSource.isPlaying)
            {
                pickupAudio.audioSource.Stop();
            }

            
            //holdingGun = false;
            heldObject = null;
            heldObjectLight = null;
            pickupImagePopup.SetActive(false);
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

                heldObjectLight = heldObject.GetComponentInChildren<Light>();
                if (heldObjectLight != null)
                {
                    heldObjectLight.enabled = false;
                }
                
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = playerCamera;
                
                pickupImagePopup.SetActive(true);
                PickupAudio pickupAudio = heldObject.GetComponent<PickupAudio>();
                if (pickupAudio != null)
                {
                    pickupAudio.PlayAudio();
                }

                PickupInfo pickupInfo = heldObject.GetComponent<PickupInfo>();
                
                string message = pickupInfo != null ? pickupInfo.pickupMessage : "You picked up: " + heldObject.name;

                if (notificationCoroutine != null)
                {
                    StopCoroutine(notificationCoroutine);
                }

                notificationCoroutine = StartCoroutine(ShowPickupNotification(message));

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
                //holdingGun = true;
            }
            //else if (hit.collider.CompareTag("Drink"))
            //{
                //ConsumableItem consumable = hit.collider.GetComponent<ConsumableItem>();
                //if (consumable != null)
                //{
                    //consumable.Consume(GetComponent<PlayerStats>());
                //}
            //}
            //else if (hit.collider.CompareTag("Food"))
            //{
                //ConsumableItem consumable = hit.collider.GetComponent<ConsumableItem>();
                //if (consumable != null)
                //{
                    //consumable.Consume(GetComponent<PlayerStats>());
                //}
            //}
            else if (hit.collider.CompareTag("Readable"))
            {
                noteDisplayPanel.SetActive(true);
                var message = hit.collider.GetComponent<Note>();
                noteTextDisplay.text = message.noteMessage;
            }
        }
    }

    private IEnumerator ShowPickupNotification(string message)
    {
        pickupNotificationText.text = message;
        pickupNotificationText.gameObject.SetActive(true);

        yield return new WaitForSeconds(notificationDuration);

        float elapsedTime = 0f;
        Color originalColor = pickupNotificationText.color;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            pickupNotificationText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - elapsedTime);
            yield return null;
        }
        
        pickupNotificationText.gameObject.SetActive(false);
        pickupNotificationText.color = originalColor;
    }

    private void StartRotatingObject()
    {
        isRotatingObject = true;
    }

    private void StopRotatingObject()
    {
        isRotatingObject = false;
    }

    private void PositionHeldObject()
    {
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * holdDistance;
        heldObject.transform.position = targetPosition;
    }

    private void RotateHeldObject()
    {
        Vector2 rotateInput = playerInput.Player.LookAround.ReadValue<Vector2>();
        Vector3 rotation = new Vector3(-rotateInput.y, rotateInput.x, 0) * rotationSpeed * Time.deltaTime;
        heldObject.transform.Rotate(rotation, Space.Self);
    }

    /*private IEnumerator ShowNoteUI()
    {
        noteDisplayPanel.SetActive(true);        
        yield return new WaitForSeconds(5.75f);
        noteTextDisplay.text = " ";
        noteDisplayPanel.SetActive(false);
        yield return null;
    }*/

    public void HideNoteUI()
    {
        noteDisplayPanel.SetActive(false);
    }
    //Check Interactability shoots ray at object to check its tag/layer(details)
    
    [Header("Animator")]
    public Animator FridgeDoor;
    public Animator FridgeDoor2;
    public Animator FridgeDoor3;
    public Animator FridgeDoor4;
    public Animator FridgeDoor5;
    public Animator FridgeDoor6;
    public Animator FridgeDoor7;
    public Animator FridgeDoor8;
    public Animator FridgeDoor9;
    public Animator FridgeDoor10;
    public Animator FridgeDoor11;
    public Animator FridgeDoor12;
    public Animator FridgeDoor13;


    [SerializeField] private int Opened, Opened2, Opened3, Opened4, Opened5, Opened6, Opened7, Opened8, Opened9, Opened10, Opened11, Opened12, Opened13;
    public LayerMask InteractLayer;
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
        else if (Physics.Raycast(ray, out hit, pickUpRange, InteractLayer))
        {
            interactableChecker.text = hit.collider.name + "Click F to interact";
            
            
        }

       
    }

    public void Interact()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, pickUpRange, InteractLayer))
        {        

            if (hit.collider.gameObject.CompareTag("Fridge") && Opened == 0)
            {
                Opened++;
                FridgeDoor.SetBool("Open", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge") && Opened == 1)
            {
                Opened--;
                FridgeDoor.SetBool("Open", false);
            }
            //Break Section
            if (hit.collider.gameObject.CompareTag("Fridge2") && Opened2 == 0)
            {
                Opened2++;
                FridgeDoor2.SetBool("Open2", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge2") && Opened2 == 1)
            {
                Opened2--;
                FridgeDoor2.SetBool("Open2", false);
            }
            //Break Section
            if (hit.collider.gameObject.CompareTag("Fridge3") && Opened3 == 0)
            {
                Opened3++;
                FridgeDoor3.SetBool("Open3", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge3") && Opened3 == 1)
            {
                Opened3--;
                FridgeDoor3.SetBool("Open3", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge4") && Opened4 == 0)
            {
                Opened4++;
                FridgeDoor4.SetBool("Open4", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge4") && Opened4 == 1)
            {
                Opened4--;
                FridgeDoor4.SetBool("Open4", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge5") && Opened5 == 0)
            {
                Opened5++;
                FridgeDoor5.SetBool("Open5", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge5") && Opened5 == 1)
            {
                Opened5--;
                FridgeDoor5.SetBool("Open5", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge6") && Opened6 == 0)
            {
                Opened6++;
                FridgeDoor6.SetBool("Open6", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge6") && Opened6 == 1)
            {
                Opened6--;
                FridgeDoor6.SetBool("Open6", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge7") && Opened7 == 0)
            {
                Opened7++;
                FridgeDoor7.SetBool("Open7", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge7") && Opened7 == 1)
            {
                Opened7--;
                FridgeDoor7.SetBool("Open7", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge8") && Opened8 == 0)
            {
                Opened8++;
                FridgeDoor8.SetBool("Open8", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge8") && Opened8 == 1)
            {
                Opened8--;
                FridgeDoor8.SetBool("Open8", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge9") && Opened9 == 0)
            {
                Opened9++;
                FridgeDoor9.SetBool("Open9", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge9") && Opened9 == 1)
            {
                Opened9--;
                FridgeDoor9.SetBool("Open9", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge10") && Opened10 == 0)
            {
                Opened10++;
                FridgeDoor10.SetBool("Open10", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge10") && Opened10 == 1)
            {
                Opened10--;
                FridgeDoor10.SetBool("Open10", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge11") && Opened11 == 0)
            {
                Opened11++;
                FridgeDoor11.SetBool("Open11", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge11") && Opened11 == 1)
            {
                Opened11--;
                FridgeDoor11.SetBool("Open11", false);
            }
            
            if (hit.collider.gameObject.CompareTag("Fridge12") && Opened12 == 0)
            {
                Opened12++;
                FridgeDoor12.SetBool("Open12", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge12") && Opened12 == 1)
            {
                Opened12--;
                FridgeDoor12.SetBool("Open12", false);
            }
            if (hit.collider.gameObject.CompareTag("Fridge13") && Opened13 == 0)
            {
                Opened13++;
                FridgeDoor13.SetBool("Open13", true);

            }
            else if (hit.collider.gameObject.CompareTag("Fridge13") && Opened13 == 1)
            {
                Opened13--;
                FridgeDoor13.SetBool("Open13", false);
            }

        }
    }
}