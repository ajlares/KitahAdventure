using System;
using System.Diagnostics.Contracts;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    // Values from input manager
    PlayerManager playerManager;
    
    [Header("Input Values")]
    public float horizontalMovement;
    public float verticalMovement;

    private Vector3 moveDirection;
    Vector3 targetRotationDirection = Vector3.zero;
    
    [Header("Movement Settings")]
    public float rotationSpeed = 5;
    
    [SerializeField] private float walkingSpeed = 2;
    [SerializeField] private float runningSpeed = 5;
    
    // Dodge/Sprint
    // bool isSprinting => PlayerInputManager.instance.sprintInput;

    
    private CharacterController characterController;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        characterController = GetComponent<CharacterController>();
    }

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRotation();
    }

    private void GetVerticalAndHorizontalInputs()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
    }
    
    private void HandleGroundedMovement()
    {
        GetVerticalAndHorizontalInputs();
        // Movement direction based on camera perspective and inputs
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (PlayerInputManager.instance.moveAmount > 0.5f)
        {
            characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.instance.moveAmount <= 0.5f)
        {
            // Move at walking speed
            characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        targetRotationDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;

        if (targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = transform.forward;
        }
        
        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotiation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime );
        transform.rotation = targetRotiation;
    }
}
