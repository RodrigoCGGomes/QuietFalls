using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for player movement logic, handling fundamental navigation.
/// This class does not dictate when the player moves but provides essential movement functionalities. 
/// The Player States and Player script determine movement behavior and invoke CharacterEngine's commands.
/// </summary>
public class CharacterEngine : MonoBehaviour
{
    #region Variables
    //References
    private NavMeshAgent agent;     //NavMeshAgentComponent attached to the player prefab.
    #endregion

    #region MonoBehaviour Calls
    private void Awake()
    {
        AssignLocalVariables();
    }
    #endregion

    #region Private Instructions
    private void AssignLocalVariables()
    {
        // Assign NavMeshAgent and check if properly assigned.
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError($"[CharacterEngine] No NavMeshAgent component in '{gameObject.name}'.");
        }
    }
    #endregion

    /// <summary> Moves the character in the specified direction with the given speed. </summary>
    /// <param name="direction"> The *global* direction in which the character should move. </param>
    /// <param name="speed"> No need to multiply by Time.deltaTime,  as the NavMeshAgent component handles it.</param>
    public virtual void Move(Vector3 direction, float speed)
    {
        agent.velocity = (direction * speed); //Genius piece of code
    }
}