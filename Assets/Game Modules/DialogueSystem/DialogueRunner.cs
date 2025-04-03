using UnityEngine;
using XNode;

public class DialogueRunner : MonoBehaviour
{
    public static DialogueRunner instance;
    public NodeGraph dialogueGraph;
    public GameObject dialogueUI;

    public void Start()
    {
        instance = this;

        DialogueGraph graph = dialogueGraph as DialogueGraph;
        if (graph != null)
        {
            graph.StartGraph(); // Your custom method
        }
        else
        {
            Debug.LogError("DialogueGraph is not assigned or is of the wrong type!");
        }
    }

    public static void ShowUI()
    {
        Debug.LogWarning("ShowUI()");
        //instance.dialogueUI.SetActive(true);
    }

    public static void HideUI()
    {
        //instance.dialogueUI.SetActive(false);
    }
}
