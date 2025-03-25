using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace QuietFallsGameManaging
{
    public class GameInputManager : MonoBehaviour
    {
        public static GameInputManager instance;
        private PlayerInput playerInput;

        [Header("Analog Inputs")]
        public Vector2 moveValue;
        public Vector2 moveValueSmoothed;
        public Vector2 lookValue, lookValueSmoothed;

        public event Action<InputAction.CallbackContext> OnMoveEvent;
        public event Action<InputAction.CallbackContext> OnConfirmEvent;
        public event Action<InputAction.CallbackContext> OnGoBackEvent;
        public event Action<InputAction.CallbackContext> OnPauseEvent;

        public void Initialize()
        {
            EnsureSingleton();
            RenameGameObject("Game Input Manager"); //Remove '(Clone)' suffix
        }

        #region Public Static Methods

        public static void RequestToBeSelected(GameObject originGO) //A Selectable is asking to be selected by QF_SelectionHandler's OnPointerEnter().
        {
            EventSystem.current.SetSelectedGameObject(originGO.transform.gameObject);
        }

        #endregion

        #region Unity Monobehaviour Calls

        private void Update()
        {
            moveValueSmoothed = Vector2.Lerp(moveValueSmoothed, moveValue, 50f * Time.deltaTime);
            lookValueSmoothed = Vector2.Lerp(lookValueSmoothed, lookValue, 50f * Time.deltaTime);
        }

        #endregion

        #region Private Instructions

        private void EnsureSingleton()
        {
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
        private void RenameGameObject(string newName)
        {
            this.gameObject.name = newName;
        }

        #endregion


        #region Listen to PlayerInput component, process values and then Invoke the events
        public void OnMove(InputAction.CallbackContext context)
        {
            //Let's read and process the values
            moveValue = context.ReadValue<Vector2>();
            //Debug.Log($"{context.control.device.deviceId} ,  {context.control.device.displayName} , {context.control.device.layout}");
            OnMoveEvent?.Invoke(context);
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            //Let's read and process the values
            lookValue = context.ReadValue<Vector2>();
        }
        public void OnConfirm(InputAction.CallbackContext context)
        {
            OnConfirmEvent?.Invoke(context);
            //Debug.Log($"{context.control.device.deviceId} ,  {context.control.device.displayName}");
        }
        public void OnGoBack(InputAction.CallbackContext context)
        {
            OnGoBackEvent?.Invoke(context);
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            OnPauseEvent?.Invoke(context);
        }
        #endregion
    }
}