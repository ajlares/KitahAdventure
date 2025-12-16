using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    PlayerInputs playerInputs;

    [Header("Player Movement")]
    public Vector2 movementInput;
    public float horizontalInput = 0;
    public float verticalInput = 0;    
    public float moveAmount;

    [Header("Camera Movement")]
    public Vector2 cameraInput;
    public float cameraHorizontalInput = 0;
    public float cameraVerticalInput = 0;
    
    [Header("Sprint / Dodge Input")]
    public bool sprintInput;
    public bool dodgeInput;

    [SerializeField] private bool bButtonPressed;
    [SerializeField] private float bButtonHoldTimer;

    [SerializeField] private float holdThreshold = 0.25f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (playerInputs == null)
        {
            playerInputs = new PlayerInputs();
    
            //Whenever this action is performed, take the values from the performed action and give them to the vector2 movementInput
            playerInputs.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInputs.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            
            // Functions for sprint and dodge
            playerInputs.PlayerMovement.RollSprint.started += _ => OnBPressed();
            playerInputs.PlayerMovement.RollSprint.canceled += _ => OnBReleased();

        }
        
        playerInputs.Enable();
    }

    private void Update()
    {
        HanddlePlayerMovementInput();
        HandleCameraMovementInput();
        HandleSprintDodgeInput();
    }

    private void HanddlePlayerMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        //Idle walking or running by clamping
        if (moveAmount <= 0.5f && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5f && moveAmount <= 1)
        {
            moveAmount = 1;
        }
    }

    private void HandleCameraMovementInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }
    
    private void HandleSprintDodgeInput()
    {
        if (bButtonPressed)
        {
            // We start the timer to see if we are running or just dodging
            bButtonHoldTimer += Time.deltaTime;

            if (bButtonHoldTimer >= holdThreshold)
            {
                sprintInput = true;
            }
        }
    }

    private void OnBPressed()
    {
        bButtonPressed = true;
        bButtonHoldTimer = 0;
    }
    
    private void OnBReleased()
    {
        if (bButtonHoldTimer < holdThreshold)
        {
            // Since we only tapped the button its a dodge
            // after we perform the dodge in locomotion, we should return this value to false
            dodgeInput = true;
        }
        bButtonPressed = false;
        sprintInput = false;
        bButtonHoldTimer = 0;
        
    }
}
