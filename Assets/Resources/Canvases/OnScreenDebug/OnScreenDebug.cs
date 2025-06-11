using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// /// DEPRECATED: This class is deprecated and will be removed in the future.
/// This class reads DebuggerAndHelper's DebugVariableList and displays the values screen texts.
/// </summary>
public class OnScreenDebug : MonoBehaviour
{
    public DebugItems debugItems;

    public void OnEnable()
    {
        // Initialize the debug items.
        debugItems.CheckForErrors();
        debugItems.UpdateText();
    }
}

[System.Serializable]
public class DebugItems
{
    [System.Serializable]
    public struct DebugItem
    {
        public string itemTitle;
        public TMP_Text textComponent;
        public int stateMachineId;
        public string stateHierarchyText;
    }

    public List<DebugItem> itemList;

    /// <summary>
    /// Checks if there is a text component attached to every single item in the DebugItems' list.
    /// </summary>
    /// <param name="list">List instance so it is accessible from a static method.</param>
    /// <returns>Returns true if an error ocurred.</returns>
    public bool CheckForErrors()
    {
        int errorCount = 0;
        for (var i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].textComponent == null)
            {
                Debug.LogError($"No text component is assigned in item [{i}] of the OnScreenDebug's items list.");
                errorCount++;
            }
        }
        if (errorCount == 0)
        {
            Debug.Log($"OnScreenDebug system initialized successfully.");
        }

        return errorCount > 0;
    }

    public void UpdateText()
    {
        foreach (var item in itemList)
        {
            item.textComponent.text = $"{item.itemTitle}:   ";
        }
    }

    /*
    public void RelayStateEntered(StateMachineInfo _info)
    {
        bool hasFoundMatchingId = false;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].stateMachineId == _info.stateMachineID)
            {
                _info.stateMachineReference.GetRootState().GetStateName();
                hasFoundMatchingId = true;

                var item = itemList[i];
                item.stateHierarchyText = "";
                itemList[i] = item;
            }
        }

        if (!hasFoundMatchingId)
        {
            Debug.LogError($"OnScreenDebug tried to Relay State entered but failed as no item in the list matches the state machine id: {_info.stateMachineID}");
        }

    }
    */
}

