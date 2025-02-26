using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TMP_Text textCurrent, textSlowCurrent;
    public int current, slowCurrent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current = (int)(1f / Time.unscaledDeltaTime);
        slowCurrent = (int)Mathf.Lerp(slowCurrent, current, Time.deltaTime * 4f);
        textCurrent.text = current.ToString();
        textSlowCurrent.text = slowCurrent.ToString();
        //Debug.LogFormat(current.ToString());
    }
}
