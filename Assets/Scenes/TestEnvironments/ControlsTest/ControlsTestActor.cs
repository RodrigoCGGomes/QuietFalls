using QuietFallsGameManaging;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsTestActor : MonoBehaviour
{
    public enum ControlType {Vector2, ButtonDown}
    public ControlType controlType;

    public enum PickInput {Move, Look, Jump}
    public PickInput pickInput;

    public GameObject child1, child2;
    public Vector3 child1PosInitial, child2PosInitial;
    public Vector3 child1EndPos, child2EndPos;

    public bool isButtonPressed;
    public float maximumRotationValue, buttonLerpSpeed;

    private void OnEnable()
    {
        maximumRotationValue = 40f;
        buttonLerpSpeed = 40f;

        GameInputManager.instance.OnConfirmEvent += OnConfirmMethod;
        child1 = transform.GetChild(0).transform.gameObject;
        child2 = child1.transform.GetChild(0).transform.gameObject;
        child1PosInitial = child1.transform.localPosition;
        child2PosInitial = child2.transform.localPosition;
        child1EndPos = Vector3.forward * -0.163f;
        child2EndPos = Vector3.forward * -0.241f;
    }

    private void OnDisable()
    {
        GameInputManager.instance.OnConfirmEvent -= OnConfirmMethod;
    }

    private void Update()
    {
        PerformMovementTick();
    }

    private void PerformMovementTick()
    {
        // Get the smoothed input from the GameInputManager
        Vector2 moveInputSmooth = QuietFallsGameManaging.GameInputManager.instance.moveValueSmoothed;

        switch (pickInput) 
        {
            case PickInput.Look:
                moveInputSmooth = QuietFallsGameManaging.GameInputManager.instance.lookValueSmoothed;
            break;
        }

        switch (controlType)
        { 
            case ControlType.Vector2:
                transform.rotation = Quaternion.Euler(maximumRotationValue * moveInputSmooth.y, maximumRotationValue * moveInputSmooth.x * -1f, 0f);
                break;
            case ControlType.ButtonDown:

                //if (isButtonPressed)
                //{
                //    child1.transform.localPosition = child1EndPos;
                //    child2.transform.localPosition = child2EndPos;
                //}
                //else
                //{
                //    child1.transform.localPosition = child1PosInitial;
                //    child2.transform.localPosition = child2PosInitial;
                //}

                if (isButtonPressed)
                {
                    child1.transform.localPosition = Vector3.Lerp(child1.transform.localPosition, child1EndPos, buttonLerpSpeed * Time.deltaTime);
                    child2.transform.localPosition = Vector3.Lerp(child2.transform.localPosition, child2EndPos, buttonLerpSpeed * Time.deltaTime);
                }
                else
                {
                    child1.transform.localPosition = Vector3.Lerp(child1.transform.localPosition, child1PosInitial, (buttonLerpSpeed * 2) * Time.deltaTime);
                    child2.transform.localPosition = Vector3.Lerp(child2.transform.localPosition, child2PosInitial, (buttonLerpSpeed * 2) * Time.deltaTime);
                }


                break;
        } 
    }



    private void OnConfirmMethod(InputAction.CallbackContext context)
    {
        // Filter for specific phases
        if (context.phase == InputActionPhase.Started)
        {
            isButtonPressed = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isButtonPressed= false;
        }
    }
}
