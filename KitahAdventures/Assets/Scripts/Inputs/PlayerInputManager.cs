using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInputManager : MonoBehaviour
{
    PlayerInputs playerInputs;

    [FormerlySerializedAs("movement")] [SerializeField] 
    private Vector2 movementInput;
    private void OnEnable()
    {
        if (playerInputs == null)
        {
            playerInputs = new PlayerInputs();

            playerInputs.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        
        playerInputs.Enable();
    }
}
