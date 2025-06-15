using UnityEngine;

/// <summary>
/// Represents a supported language in the game, including its code, display name and language type enum.
/// Used to store and identify language-related data for localization systems.
/// </summary>
public class GameLanguage
{
    #region Variables
    /// <summary>
    /// ISO code or internal identifier (e.g. "en", "pt", "de").
    /// </summary>
    public string code;

    /// <summary>
    /// Human-readable name of the language (e.g. "English", "Português", "Deutsch").
    /// </summary>
    public string displayName;

    /// <summary>
    /// Corresponding enum value for the language.
    /// </summary>
    public GameLanguageType type;
    #endregion
}