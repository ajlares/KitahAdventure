using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerLocomotionManager playerLocomotionManager;
    private void Awake()
    {
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }
    
    private void Update()
    {
        playerLocomotionManager.horizontalMovement = PlayerInputManager.instance.movementInput.x;
        playerLocomotionManager.verticalMovement = PlayerInputManager.instance.movementInput.y;

        playerLocomotionManager.HandleAllMovement();
    }
}
