using UnityEngine;
using XNodeEditor;

/// <summary>
/// The purpose of this class is to change the title of the window to the name of the graph.
/// By default XNode does not show the title of the graph you're working on.
/// </summary>
[CustomNodeGraphEditor(typeof(DialogueGraph))] // Or NodeGraph if you want all graphs
public class DialogueGraphEditor : NodeGraphEditor
{
    public override void OnGUI()
    {
        if (NodeEditorWindow.current != null && target != null)
        {
            NodeEditorWindow.current.titleContent = new GUIContent(target.name);
        }
        base.OnGUI();
    }
}