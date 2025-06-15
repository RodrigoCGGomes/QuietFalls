using System;
using UnityEngine;

/// <summary>
/// TranslatedEntry is a serializable class that does nothing by itself. It is used to store a translation for a specific language.
/// For example: TextMeshPro_Localizer contains a List of TranslatedEntry, so that translations can be added in the inspector.
/// </summary>
[Serializable]
public class TranslatedEntry
{
    public GameLanguageType language;
    [TextArea]
    public string text;
}