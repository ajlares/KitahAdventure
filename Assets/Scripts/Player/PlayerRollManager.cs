using UnityEngine;

public class PlayerRollManager : MonoBehaviour
{
    [Header("Roll Settings")]
    [SerializeField] private float rollDistance = 3;
    [SerializeField] private float rollDuration = 0.1f;

    private bool isRolling;
    private Vector3 rollDirection;
    private float rollTimer;

    private CharacterController characterController;

    // FOr read only
    public bool IsRolling => isRolling;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Detect roll input
        if (PlayerInputManager.instance.dodgeInput && !isRolling && PlayerStatsManager.instance.HasEnoughStamina())
        {
            StartRoll();
        }

        if (isRolling)
        {
            HandleRollMovement();
        }
    }

    private void StartRoll()
    {
        isRolling = true;
        rollTimer = 0f;

        PlayerStatsManager.instance.ConsumeDodgeStamina();
        SetRollDirection();
        
        PlayerInputManager.instance.dodgeInput= false;
        
        // To do: give player i frames
    }

    private void SetRollDirection()
    {
        Vector3 inputDir = 
            PlayerCamera.instance.transform.forward * PlayerInputManager.instance.verticalInput + 
            PlayerCamera.instance.transform.right * PlayerInputManager.instance.horizontalInput;

        inputDir.y = 0f;

        if (inputDir.magnitude > 0.1f)
            rollDirection = inputDir.normalized;
        else
            rollDirection = (transform.forward * -1f);
    }

    private void HandleRollMovement()
    {
        rollTimer += Time.deltaTime;

        float moveSpeed = rollDistance / rollDuration;
        characterController.Move(rollDirection * moveSpeed * Time.deltaTime);

        if (rollTimer >= rollDuration)
        {
            EndRoll();
        }
    }

    private void EndRoll()
    {
        isRolling = false;
        // end i frames
    }
}