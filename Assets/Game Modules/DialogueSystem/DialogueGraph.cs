using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "New Dialogue Graph", menuName = "Dialogue/Graph")]
public class DialogueGraph : NodeGraph {
	public DialogueNode startNode;
	public DialogueNode currentNode;

    public void StartGraph()
    {
        currentNode = startNode;
        Execute();
    }

    public void Execute()
	{
		currentNode.Execute();
	}
}