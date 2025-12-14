using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerLocomotionManager playerLocomotionManager;
    private void Awake()
    {
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
