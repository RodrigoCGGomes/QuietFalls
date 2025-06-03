using System;
using System.Collections.Generic;

public abstract class BaseState<T> where T : BaseState<T>
{
    protected BaseStateMachine<T> Context { get; }
    protected object Factory { get; }
    public List<BaseState<T>> subStateList { get; protected set; }
    public BaseState<T> CurrentSuperState { get; protected set; }

    protected BaseState(BaseStateMachine<T> context, bool isRoot)
    {
        Context = context;
        Factory = GetFactory(context);
        subStateList = new List<BaseState<T>>();
    }

    protected virtual object GetFactory(BaseStateMachine<T> context)
    {
        throw new InvalidOperationException("GetFactory must be implemented by derived state classes.");
    }

    public abstract void EnterState();
    public abstract void Tick();
    public abstract void ExitState();
    public virtual void InitializeSubState() { }

    public void CascadeTick()
    {
        Tick();
        foreach (var subState in subStateList)
        {
            subState.CascadeTick();
        }
    }
}