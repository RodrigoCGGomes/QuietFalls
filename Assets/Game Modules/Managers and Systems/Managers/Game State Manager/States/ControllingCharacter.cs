using UnityEditor;
using UnityEngine;

public class ControllingCharacter : GameState
{
    public ControllingCharacter(bool isRoot) : base(isRoot)
    {
        stateName = "Controlling Character State";
    }

    public override void EnterState()
    {
        Debug.Log($"ControllingCharacter.EnterState(); - {this}, SuperState = {CurrentSuperState}");
    }

    public override void Tick() { }

    public override void ExitState() { }
}