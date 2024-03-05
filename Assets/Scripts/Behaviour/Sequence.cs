using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BTNode
{
    private List<BTNode> myNodes = new List<BTNode>();

    // Constructor: Initializes a new instance of the Sequence with a list of BTNode children.
    public Sequence (List<BTNode> nodes)
    {
        myNodes = nodes;
    }

    // Evaluate: Processes each child node in sequence. Returns SUCCESS if all children succeed,
    // FAILURE if any child fails, and RUNNING if any child is still running.
    public override BTNodeState Evaluate()
    {
        bool childRunning = false;

        foreach (BTNode node in myNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeState.FAILURE:
                    currentNodeState = BTNodeState.FAILURE;
                    return currentNodeState; // Immediately returns FAILURE if any child fails.

                case BTNodeState.SUCCESS:
                    continue; // Moves to the next child node if the current one succeeds.

                case BTNodeState.RUNNING:
                    childRunning = true; // Marks that at least one child is still running.
                    continue;

                default:
                    currentNodeState = BTNodeState.SUCCESS;
                    return currentNodeState; // Defaults to SUCCESS if no specific state is matched.

            }
        }
        // Sets the state to RUNNING if any child was running, otherwise SUCCESS.
        currentNodeState = childRunning ? BTNodeState.RUNNING : BTNodeState.SUCCESS;
        return currentNodeState;

    }
}