using QuietFallsGameManaging;
using System.Collections.Generic;
using UnityEngine;

public class GameDebugger : MonoBehaviour
{
    public static GameDebugger instance;
    public enum DebugLevel {Always, Trivial, Important, VeryImportant}
    public DebugLevel debugLevel;

    public void Initialize()
    { 
        instance = this;
    }

    public static void SetDebugLevel(DebugLevel paramDebugLevel)
    {
        GameDebugger.instance.debugLevel = paramDebugLevel;
    }
}

