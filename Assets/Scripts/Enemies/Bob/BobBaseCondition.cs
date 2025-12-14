using UnityEngine;

public class BobBaseCondition : ScriptableObject
{
    public virtual bool Check(BobStateMachine stateMachine)
    {
        return false;
    }
}

[System.Serializable]
public class BobTransition
{
    public BobBaseCondition condicion;
    public BobBaseState state;
}