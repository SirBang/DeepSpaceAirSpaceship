using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetTask : BTNode
{
    IBehaviourAI myAI; // Reference to the AI behavior interface.
    Transform agentTransform; // Transform component of the agent.
    Vector3 targetPosition; // The current target position the agent is moving towards.
    float range; // The range within which the agent should stop moving towards the target.
    event InputEventFloat ForwardEvent; // Event to trigger movement, with the float parameter indicating the thrust or intensity of the movement.

    // Constructor for initializing the MoveToTargetTask with necessary parameters.
    public MoveToTargetTask (IBehaviourAI _myAI, float _range, InputEventFloat _forwardEvent)
    {
        myAI = _myAI; // Assigning the AI behavior interface.
        range = _range; // Setting the stopping range.
        ForwardEvent = _forwardEvent; // Setting the forward movement event.
    }

    // Evaluates the task, moving the agent towards the target and triggering the ForwardEvent with calculated thrust.
    public override BTNodeState Evaluate()
    {
        Vector3 agentPosition = myAI.GetAgentTransform().position; // Getting the current position of the agent.
        targetPosition = myAI.GetTargetPosition(); // Getting the current target position.

        float distance = Vector3.Distance(agentPosition, targetPosition); // Calculating the distance to the target.
        float thrust = distance / range; // Calculating the thrust based on distance and range.
        if (ForwardEvent != null) ForwardEvent(thrust); // Triggering the ForwardEvent if it's not null.

        return BTNodeState.SUCCESS; // Returning SUCCESS state indicating the task was successfully evaluated.
    }
}
