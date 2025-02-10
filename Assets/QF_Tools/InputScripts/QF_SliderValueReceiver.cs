using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class QF_SliderValueReceiver : MonoBehaviour
{
    private TMP_Text textMeshProText;
    public enum DisplayType {IntegerNumbers, F1}
    public DisplayType displayType;

    #region MonoBehaviour Calls
    private void Awake()
    {
        textMeshProText = GetComponent<TMP_Text>();
    }
    #endregion

    #region Public Instructions
    public void SelfUpdateText(float value)
    {
        switch (displayType)
        { 
            case DisplayType.IntegerNumbers:
                textMeshProText.text = ((int)value).ToString();
                break;
            case DisplayType.F1:
                textMeshProText.text = value.ToString("F1");
                break;
        } 
    }
    #endregion
}
