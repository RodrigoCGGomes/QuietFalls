using UnityEngine;

namespace GameModules.Systems
{
    /// <summary>
    /// Preferences System is the system responsible to load and save language, video and audio settings.
    /// The entry point to this system is the GameObject instantiated from GameManager.SpawnGameManagers().
    /// </summary>
    public class GamePreferencesSystem : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the Preference System.
        /// </summary>
        public static GamePreferencesSystem instance;

        public LanguageSettings languageSettings;

        /// <summary>
        /// Initial setup of the Preferences System. Called only once by GameManager.SetUpGameManagers() at the start of the game.
        /// </summary>
        public void Initialize()
        {
            languageSettings = new LanguageSettings(GameLanguageType.German);
        }
    }
}