using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    PlayerInputs playerInputs;

    public Vector2 movementInput;
    [SerializeField] private float horizontalInput = 0;
    [SerializeField] private float verticlaInput = 0;
    public float moveAmount;

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
        }
        
        playerInputs.Enable();
    }

    private void Update()
    {
        HanddleMovementInput();
    }

    private void HanddleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticlaInput = movementInput.y;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticlaInput));
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
}
