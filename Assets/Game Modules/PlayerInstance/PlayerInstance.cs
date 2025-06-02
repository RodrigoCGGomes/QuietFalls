using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// PlayerInstance.cs represents a MonoBehaviour that exists in the scene. 
/// The core idea is that each player in the game corresponds to a PlayerInstance. 
/// This component is responsible for determining whether the player's character prefab 
/// is actively present in the scene or if the instance simply tracks the player's presence in the game.
/// </summary>
public class PlayerInstance : MonoBehaviour
{
    [Header("Player Information")]
    public int playerId;
    public string playerName;

    [Header("Input")]
    public PlayerInput playerInput;
}
