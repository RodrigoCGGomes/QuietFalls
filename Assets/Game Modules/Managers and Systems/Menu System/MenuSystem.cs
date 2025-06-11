using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Menu system is responsible for showing needed UI.
/// </summary>
public class MenuSystem : MonoBehaviour
{
    private static MenuSystem instance;

    /// <summary>
    /// This canvas is scene persistent and contains all in-game menus.
    /// </summary>
    public Canvas inGameCanvases;

    /// <summary>
    /// List of In-Game menus (children of inGameCanvases). 
    /// Todo: Later have all canvases inherit from a base class and move the logic there.
    /// </summary>
    public List<GameObject> inGameMenus;



    public static void HideAllInGameCanvases()
    {
        foreach (GameObject menu in instance.inGameMenus)
        {
            menu.SetActive(false);
        }
    }

    public static void ShowAllInGameCanvases()
    {
        foreach (GameObject menu in instance.inGameMenus)
        {
            menu.SetActive(true);
        }
    }

    public static void ShowDialogueUI() 
    {
    
    }

}
