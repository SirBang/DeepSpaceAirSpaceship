using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BTNode : MonoBehaviour
{
    protected BTNodeState currentNodeState;

    // Property to access the current state of the node.
    public BTNodeState nodeState
    {
        get { return currentNodeState; }
    }

    // Constructor for the BTNode. It's empty because this is an abstract class meant to be inherited by specific behavior tree nodes.
    public BTNode() { }

    // Abstract method Evaluate that must be implemented by inheriting classes. This method determines the node's state (e.g., success, failure) based on its logic.
    public abstract BTNodeState Evaluate();
}