public class StateMachineInfo
{
    public GameStateMachine stateMachineReference;
    public string stateMachineName;
    public int stateMachineID;

    public StateMachineInfo(GameStateMachine _reference, string _stateMachineName, int _stateMachineID)
    {
        stateMachineReference = _reference;
        stateMachineName = _stateMachineName;
        this.stateMachineID = _stateMachineID;
    }
}