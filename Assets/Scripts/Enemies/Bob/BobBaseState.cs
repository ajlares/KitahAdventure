using UnityEngine;
public abstract class BobBaseState : ScriptableObject
{
    public BobTransition[] transitions;
    
    public virtual void EnterState(BobStateMachine stateMachine)
    {
        
    }

    public virtual void UpdateState(BobStateMachine stateMachine)
    {
        
    }

    public virtual void ExitState(BobStateMachine stateMachine)
    {
        
    }

    public void CheckTransition(BobStateMachine stateMachine)
    {
        if (transitions.Length > 0)
        {
            foreach (BobTransition transition in transitions)
            {
                if (transition.condicion != null && transition.condicion.Check(stateMachine))
                {
                    stateMachine.ChangeState(transition.state);
                    break;
                }
            }
        }
    }
}