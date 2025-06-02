using UnityEngine;

public class DialogueBlock : PlayerState
{
    public DialogueBlock(GamePlayer parPlayer) : base(parPlayer)
    {

    }

    public override void EnterState()
    {
        Debug.Log($"PlayerState.EnterState(); - {this}");
        DialogueRunner.ShowUI();
    }

    public override void ExitState()
    {
        DialogueRunner.HideUI();
    }

    public override void Tick()
    {
        
    }
}