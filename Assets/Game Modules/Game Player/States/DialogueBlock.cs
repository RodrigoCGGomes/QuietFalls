using UnityEngine;

public class DialogueBlock : PlayerState
{
    public DialogueBlock(GamePlayer parPlayer) : base(parPlayer)
    {
        Debug.Log("DialogueBlock player state activated!");
    }

    public override void EnterState()
    {
        DialogueRunner.ShowUI();
        Debug.LogWarning("GamePlayer instance entered into Dialogue Block PlayerState");
    }

    public override void ExitState()
    {
        DialogueRunner.HideUI();
    }

    public override void Tick()
    {
        
    }
}