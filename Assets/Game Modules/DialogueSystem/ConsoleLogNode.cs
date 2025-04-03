using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : Node
{

    [Input] [TextArea] public string input;
	public enum EnumTest { A, B, C }
	[Input] public EnumTest enumInput;
    [Output] public float output;

    // Use this for initialization
    protected override void Init() {
		base.Init();
		
	}

	public virtual void Execute()
	{
		// Debug.Log("Executing ConsoleLog node!");
	}

	public void NextNode(string _exit)
	{
		DialogueNode dialogue = null;
		foreach (NodePort port in this.Ports)
		{ 
			if(port.fieldName == _exit)
            {
                dialogue = port.Connection.node as DialogueNode;
				break;
            }
        }
        if (dialogue != null)
        {
			DialogueGraph graph = this.graph as DialogueGraph;
            graph.currentNode = dialogue;
			graph.Execute();

        }
    }


	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null;
	}
}