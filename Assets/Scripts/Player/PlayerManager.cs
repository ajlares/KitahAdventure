using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerLocomotionManager playerLocomotionManager;
    PlayerStateManager playerStateManager;
    private void Awake()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    private void Start()
    {
        PlayerCamera.instance.player = this;
    }

    private void Update()
    {
        playerLocomotionManager.HandleAllMovement();
        //PlayerCamera.instance.HandleAllCameraActions();
    }
    
    private void LateUpdate()
    {
        PlayerCamera.instance.HandleAllCameraActions();
    }

}
