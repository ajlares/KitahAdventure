using UnityEngine;

[CreateAssetMenu(fileName = "BobIdleState", menuName = "Enemies/Bob/IdleState")]
public class BobIdleState : BobBaseState
{
    public override void EnterState(BobStateMachine stateMachine)
    {
        
    }

    public override void UpdateState(BobStateMachine stateMachine)
    {
        stateMachine.ChangeAnimation("Idle");
    }

    public override void ExitState(BobStateMachine stateMachine)
    {
        
    }
}
