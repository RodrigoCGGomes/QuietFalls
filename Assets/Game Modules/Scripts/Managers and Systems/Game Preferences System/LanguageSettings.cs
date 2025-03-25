using System.Collections.Generic;
using UnityEngine;

public class LanguageSettings
{
    public static LanguageSettings instance;
    private GameLanguage currentLanguage;

    /// <summary>
    /// List of available languages in the game.
    /// </summary>
    private Dictionary<GameLanguageType, GameLanguage> languages = new Dictionary<GameLanguageType, GameLanguage>
    {
        { GameLanguageType.English, new GameLanguage { type = GameLanguageType.English, code = "en", displayName = "English" } },
        { GameLanguageType.Portuguese, new GameLanguage { type = GameLanguageType.Portuguese, code = "pt", displayName = "Português"} },
        { GameLanguageType.German, new GameLanguage { type = GameLanguageType.German, code = "de", displayName = "Deutsch" } },

        // Add new languages here... (Hard coding is usually not the best approach, however, for this case it is fine.)
    };

    #region
    private LanguageSettings ()
    {
        // Private default constructor to prevent instantiation without passing the required parameters.
    }

    /// <summary>
    /// LanguageSettings constructor.
    /// </summary>
    /// <param name="defaultLanguage"> Default language to be used if no language preference is found. </param>
    public LanguageSettings(GameLanguageType defaultLanguage)
    {
        instance = this;
        if (!PlayerPrefs.HasKey("language"))
        {
            // If there is no language preference saved, set the default language and return.
            currentLanguage = languages[defaultLanguage]; // Default language
            Debug.LogWarning($"Since there was no language preference saved, language was set to the default value: {currentLanguage.displayName}");
            return;
        }
        
        string savedCode = PlayerPrefs.GetString("language");

        foreach (var lang in languages.Values)
        {
            if (lang.code.Equals(savedCode, System.StringComparison.OrdinalIgnoreCase))
            {
                currentLanguage = lang;
                Debug.LogWarning($"Language preference loaded: {currentLanguage.displayName}");
            }
        }

        //Debug.LogWarning(msg1 + msg2);
    }
    #endregion

    #region Public Static Methods (publicly accessed)

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newLanguage"></param>
    public static void SetLanguageAndSavePrefs(string newLanguage)
    {
        foreach (var lang in instance.languages.Values)
        {
            if (lang.code.Equals(newLanguage, System.StringComparison.OrdinalIgnoreCase))
            {
                instance.currentLanguage = lang;
                PlayerPrefs.SetString("language", lang.code);
                Debug.LogWarning($"Language preference saved: {instance.currentLanguage.displayName}");
                return;
            }
        }
        Debug.LogError($"Failed to find a language with the code '{newLanguage}'");
    }

    /// <summary>
    /// Returns the value (GameLanguage) of the current language being used in the game.
    /// </summary>
    public static GameLanguage GetCurrentLanguage()
    {
        return instance.currentLanguage;
    }
    #endregion
}