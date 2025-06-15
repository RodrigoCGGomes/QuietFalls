using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshPro_Translator : MonoBehaviour
{
    public List<TranslatedEntry> translatedEntries;

    public void OnEnable()
    {
        Translate();
    }

    private void Translate()
    {
        TMP_Text textComponent = GetComponent<TMP_Text>();
        GameLanguage currentLanguage = LanguageSettings.GetCurrentLanguage();
        foreach (var entry in translatedEntries)
        {
            if (entry.language == currentLanguage.type)
            { 
                if(textComponent != null)
                {
                    textComponent.text = entry.text;
                }
            }
        }
    }
}