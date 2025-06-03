using System;
using System.Collections.Generic;

public abstract class BaseState
{

    public StateInfo stateInfo; // Information about the state, such as name and description

    protected object Context { get; } // State machine (GameStateMachine, PlayerStateMachine, etc.)
    protected object Factory { get; } // Factory for creating states
    public List<BaseState> subStateList { get; protected set; }
    public BaseState CurrentSuperState { get; protected set; }

    protected BaseState(BaseStateMachine<BaseState> context, bool isRoot)
    {
        Context = context;
        Factory = GetFactory(context);
    }

    protected virtual object GetFactory(object context)
    {
        throw new InvalidOperationException("GetFactory must be implemented by derived state classes.");
    }

    public abstract void EnterState();
    public abstract void Tick();
    public abstract void ExitState();
    public virtual void InitializeSubState() { }


    /// <summary>
    /// CascadeTick is used to call the abstract Tick on the current state and all sub-states recursively.
    /// </summary>
    public void CascadeTick()
    {
        Tick();
        foreach (var subState in subStateList)
        {
            subState.CascadeTick();
        }
    }


}