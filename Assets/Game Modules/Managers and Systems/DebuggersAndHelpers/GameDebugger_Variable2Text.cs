using TMPro;
using UnityEngine;

public class GameDebugger_Variable2Text : MonoBehaviour
{
    private TMP_Text textComponent;
    public string variableName, optionalPrefix;

    public void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    public void Update()
    {
        textComponent.text = optionalPrefix + GameDebugger.ReadVariable(variableName);

    }
}
