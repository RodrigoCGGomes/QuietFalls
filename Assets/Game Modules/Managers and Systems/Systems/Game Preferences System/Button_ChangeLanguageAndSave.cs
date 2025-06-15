using UnityEngine;

public class Button_ChangeLanguageAndSave : MonoBehaviour
{
    public string languageCode;

    public void ChangeLanguageAndSave()
    { 
        LanguageSettings.SetLanguageAndSavePrefs(languageCode);
    }
}
