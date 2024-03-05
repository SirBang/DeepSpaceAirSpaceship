using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class defines a behavior tree node for checking if an agent has arrived at a target location.
public class CheckArrivalTask : BTNode
{
    // Reference to the AI behavior interface.
    IBehaviourAI myAI;

    // Constructor to initialize the CheckArrivalTask with a specific AI agent.
    public CheckArrivalTask(IBehaviourAI _myAI)
    {
        myAI = _myAI;
    }

    // Evaluates the node's condition, determining if the agent has reached its target.
    public override BTNodeState Evaluate()
    {
        // Get the current position of the agent and the target position.
        Vector3 agentPosition = myAI.GetAgentTransform().position;
        Vector3 targetPosition = myAI.GetTargetPosition();

        // Calculate the distance between the agent and the target.
        float distance = Vector3.Distance(agentPosition, targetPosition);

        // If the distance is less than 100 units, the task is considered successful.
        if (distance < 100f)
        {
            return BTNodeState.SUCCESS;
        }
        else // Otherwise, the task has failed.
        {
            return BTNodeState.FAILURE;
        }
    }
}