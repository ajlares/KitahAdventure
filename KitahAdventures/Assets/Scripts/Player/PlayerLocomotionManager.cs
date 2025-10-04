using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    // Values from input manager
    PlayerManager playerManager;
    public float horizontalMovement;
    public float verticalMovement;

    private Vector3 moveDirection;
    [SerializeField] private float walkingSpeed = 2;
    [SerializeField] private float runningSpeed = 5;
    
    private CharacterController characterController;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        characterController = GetComponent<CharacterController>();
    }

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
    }
    
    private void HandleGroundedMovement()
    {
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
}
