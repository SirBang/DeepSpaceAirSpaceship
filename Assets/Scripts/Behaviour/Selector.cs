using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BTNode
{
    // List to hold child nodes for the selector.
    protected List<BTNode> myNodes = new List<BTNode>();

    // Constructor to initialize the selector with a list of child nodes.
    public Selector (List<BTNode> nodes)
    {
        myNodes = nodes;
    }

    // Evaluates the child nodes in order and returns SUCCESS if any child node succeeds.
    public override BTNodeState Evaluate()
    {
        foreach (BTNode node in myNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeState.FAILURE:
                    // If the current node fails, continue to the next node.
                    continue;

                case BTNodeState.SUCCESS:
                    // If any node succeeds, the selector succeeds.
                    currentNodeState = BTNodeState.SUCCESS;
                    return currentNodeState;

                default:
                    // Continue to the next node if the current node is not FAILURE or SUCCESS.
                    continue;
            }
        }

        // If all nodes fail, the selector fails.
        currentNodeState = BTNodeState.FAILURE;
        return currentNodeState;
    }
}