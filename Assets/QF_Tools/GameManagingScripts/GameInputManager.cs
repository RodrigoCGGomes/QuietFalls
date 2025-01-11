using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuietFallsGameManaging {
    public class GameInputManager : MonoBehaviour
    {
        public static GameInputManager instance;
        private PlayerInput playerInput;

        public Vector2 moveValue, moveValueSmoothed;
        public Vector2 lookValue, lookValueSmoothed;

        public event Action<InputAction.CallbackContext> OnJumpEvent;

        public void Initialize()
        {
            EnsureSingleton();
        }

        #region Unity Monobehaviour Calls
        private void Update()
        {
            moveValueSmoothed = Vector2.Lerp(moveValueSmoothed, moveValue, 10f * Time.deltaTime);
            lookValueSmoothed = Vector2.Lerp(lookValueSmoothed, lookValue, 10f * Time.deltaTime);
        }
        #endregion

        private void EnsureSingleton()
        {
            Debug.Log("GameInputManager.EnsureSingleton();");
            if (instance != null && instance != this)
            {
                Debug.LogError("Somehow more than one GameInputManager instance exists. Being deleted.");
                Destroy(gameObject);
                return;
            }
            instance = this;

            // Verifica e configura o PlayerInput
            playerInput = GetComponent<PlayerInput>();
            if (playerInput == null)
            {
                playerInput = gameObject.AddComponent<PlayerInput>();
            }

            playerInput.actions = Resources.Load<InputActionAsset>("InputActions/InputSystem_Actions");
            if (playerInput.actions == null)
            {
                Debug.LogError("Couldn't find the game InputAction from Resources folder.");
            }
        }

        #region Listen to PlayerInput component, process values and then Invoke the events
        public void OnMove(InputAction.CallbackContext context)
        {
            //Let's read and process the values
            moveValue = context.ReadValue<Vector2>();
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            //Let's read and process the values
            lookValue = context.ReadValue<Vector2>();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            OnJumpEvent?.Invoke(context);
            
        }


        #endregion
    }
}