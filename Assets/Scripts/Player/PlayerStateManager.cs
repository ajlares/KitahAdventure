using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; } = PlayerState.Normal;

    public void SetState(PlayerState newState)
    {
        CurrentState = newState;
    }

    public bool IsState(PlayerState state)
    {
        return CurrentState == state;
    }

    public bool CanMove()
    {
        return CurrentState == PlayerState.Normal;
    }

    public bool CanRotate()
    {
        return CurrentState == PlayerState.Normal;
    }
}