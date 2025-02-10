using QuietFallsGameManaging;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CANVAS_SplitScreenSelection : MonoBehaviour
{
    #region Variables
    public TMP_Text playersAreReadyText;
    private Color originalColor;
    public Color readyColor;
    public SideSelection leftSide, rightSide;
    public bool arePlayersReady;
    #endregion

    #region On Enable/Disable
    private void OnEnable()
    {
        //Subscribe to Inputs
        GameInputManager.instance.OnConfirmEvent += OnConfirmEventHandler;
        GameInputManager.instance.OnGoBackEvent += PressedBack;

        //Store original texts and color to know what change to when unsigning a device
        leftSide.StoreOriginalText();
        rightSide.StoreOriginalText();
        originalColor = leftSide.imageComponent.color;

        //Ready indication text starts hidden and it shown upon both players being ready.
        playersAreReadyText.gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        //Unsubscribe to Input events to prevent memory leak
        GameInputManager.instance.OnConfirmEvent += OnConfirmEventHandler;
        GameInputManager.instance.OnGoBackEvent += PressedBack;
    }
    #endregion

    #region Input Event Handlers
    private void OnConfirmEventHandler(InputAction.CallbackContext context)
    {
        SideSelection side = leftSide;
        GameDeviceManager.GameDevice.AssignedPlayer playerToAssignTo;

        if (context.phase == InputActionPhase.Started)
        {
            if (arePlayersReady)
            {
                GameDeviceManager.RegisterDevices(leftSide.device.device, rightSide.device.device);
                SceneManager.LoadScene("SampleScene");
            }

            if (IsDeviceAlreadyAssigned(context.control.device) == false) {
                Debug.Log("Someone asked to be assigned");
                if (leftSide.occupied == false)
                {
                    side = leftSide;
                    playerToAssignTo = GameDeviceManager.GameDevice.AssignedPlayer.Player1;
                }
                else
                {
                    side = rightSide;
                    playerToAssignTo = GameDeviceManager.GameDevice.AssignedPlayer.Player1;
                }

                side.Assign(new GameDeviceManager.GameDevice(context.control.device, playerToAssignTo), readyColor);
                CheckIfBothReady();

            }
        }
    }
    private void PressedBack(InputAction.CallbackContext context)
    {
        SideSelection sideToDelete = null;
        if (context.phase == InputActionPhase.Started) 
        {
            /*Debug.Log($"Debug Leftside: Ocupied = {leftSide.occupied}," +
                $" Device != null : {leftSide.device != null}," +
                $" matches : {leftSide.device.device == context.control.device}");
            */

            if (leftSide.occupied && leftSide.device != null && leftSide.device.device == context.control.device)
            {
                sideToDelete = leftSide;
            }
            if (rightSide.occupied && rightSide.device != null && rightSide.device.device == context.control.device)
            {
                sideToDelete = rightSide;
            }

            sideToDelete?.Unassign(originalColor);
            CheckIfBothReady();
        }
    }
    #endregion

    #region Private Instructions
    private bool IsDeviceAlreadyAssigned(InputDevice deviceToCheck)
    {
        bool result = false;
        if (leftSide.occupied == true && leftSide.device.device == deviceToCheck) result = true;
        if (rightSide.occupied == true && rightSide.device.device == deviceToCheck) result = true;
        return result;
    }
    private void CheckIfBothReady()
    {
        if (leftSide.occupied == true && rightSide.occupied == true)
        {
            Debug.LogWarning("Both Players are ready!");
            playersAreReadyText.gameObject.SetActive(true);
            arePlayersReady = true;
        }
        else
        {
            Debug.LogWarning("Players are no longer ready");
            playersAreReadyText.gameObject.SetActive(false);
            arePlayersReady = false;
        }
    }
    #endregion 
}

[System.Serializable]
public class SideSelection
{
    public string sideName;
    public TMP_Text textComponent;
    public Image imageComponent;
    public string originalText;
    public bool occupied;
    public GameDeviceManager.GameDevice device;

    public SideSelection()
    {

    }
    public void StoreOriginalText()
    {
        if (originalText != null)
        {
            originalText = textComponent.text;
        }
        else
        {
            Debug.LogWarning($"Please assign the text component of {sideName}");
        }
    }
    public void Assign(GameDeviceManager.GameDevice device, Color panelColor)
    {
        Debug.Log($"Assigned {sideName} to {device.deviceName}");
        occupied = true;
        this.device = device;
        textComponent.text = device.deviceName;
        imageComponent.color = panelColor;
    }

    public void Unassign(Color panelColor)
    {
        if(occupied == false)
        {
            Debug.LogWarning("Trying to unassign an unoccupied slot");
            return;
        }
        Debug.Log($"Unassigned {device.deviceName} from {sideName}");
        occupied = false;
        device = null;
        textComponent.text = originalText;
        imageComponent.color = panelColor;
    }
}
