using System;
using UnityEngine;

public class BobStateMachine : MonoBehaviour
{
    [Header("----- setttups-----")]
    public BobBaseState initialState;
    public BobBaseState CurrentState;
    public BlackBoard BobBlackBoard;

    private void Start()
    {
        ChangeState(initialState);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
        CurrentState.CheckTransition(this);
    }

    public void ChangeState(BobBaseState newState)
    {
        if(CurrentState == newState || newState == null)
        {
            return;
        }
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);
        }
        CurrentState = newState;
        CurrentState.EnterState(this);
    }
}
