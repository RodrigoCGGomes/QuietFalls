using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows for registering, modifying and reading debug variables to help in the game development process.
/// </summary>
public class GameDebugger : MonoBehaviour
{
    public static GameDebugger instance;
    public List<GameDebuggerVariable> variables;

    /// <summary>
    /// Initialization instructions called by GameManager.cs at the very start of the game.
    /// </summary>
    public void Initialize()
    {
        instance = this;
        variables = new List<GameDebuggerVariable>();
    }

    /// <summary>
    /// Registers/Modifies a new debug variable depending on whether it already exists.
    /// </summary>
    public static void WriteVariable(string variableName, string variableValue)
    {
        // Logs a write attempt.
        Debug.Log($"GameDebugger.RegisterVariable('{variableName} : {variableValue}');");

        // Error Check for instance's existence.
        if (instance == null)
        {
            Debug.LogError("GameDebugger instance is not initialized. Please call Initialize() first.");    
            return;
        }

        bool variableExists = false;
        foreach (var variable in instance.variables)
        {
            if (variable.code == variableName)
            {
                variable.value = variableValue;
                variableExists = true;
                Debug.Log($"GameDebugger: Updated existing variable '{variableName}' to '{variableValue}'.");
                break;
            }
        }

        if (variableExists == false)
        {
            instance.variables.Add(new GameDebuggerVariable
            {
                code = variableName,
                value = variableValue
            });
        }
    }

    public static string ReadVariable(string variableName)
    {
        string result = "Variable not found";
        foreach (var variable in instance.variables)
        {
            if (variable.code == variableName)
            {
                result = variable.value;
                return result;
            }
        }
        return result;
    }
}