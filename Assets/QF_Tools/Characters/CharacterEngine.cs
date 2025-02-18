using UnityEngine;
using UnityEngine.AI;


public class CharacterEngine : MonoBehaviour
{

    [Header ("Character Engine")]
    public CharacterEngineState currentState;

    private NavMeshAgent agent;


    private void Awake()
    {
        AssignNavMeshAgent();
    }

    private void AssignNavMeshAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError($"[CharacterEngine] No NavMeshAgent component in '{gameObject.name}'.");
        }
    }

    // Método para mudar de estado
    public virtual void ChangeState(CharacterEngineState newState)
    {
        currentState = newState;
    }

    // Método de movimentação baseado no estado atual
    public virtual void Move(Vector3 direction, float speed)
    {
        if (currentState == CharacterEngineState.FreeToMove)
        {
            agent.velocity = (direction * speed); // voltar com o time.deltatime no lugar do 0.1f
        }
        //Debug.LogWarning("CharacterEngineMove");
    }

}

public enum CharacterEngineState
{ 
    FreeToMove,
    MovingTowardsTarget,
    Blocked
}