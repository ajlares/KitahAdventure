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
        }
        
        playerInputs.Enable();
    }

    private void Update()
    {
        HanddlePlayerMovementInput();
        HandleCameraMovementInput();
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
}
