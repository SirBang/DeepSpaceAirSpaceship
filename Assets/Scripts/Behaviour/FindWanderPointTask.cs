using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class defines a behavior tree node for finding a wander point within a specified range.
public class FindWanderPointTask : BTNode
{
    float range; // The maximum distance from the current position within which to find a wander point.
    IBehaviourAI myAI; // Reference to the AI behavior interface.

    // Constructor initializing the AI interface and range for the wander point.
    public FindWanderPointTask(IBehaviourAI _myAI, float _range)
    {
        range = _range;
        myAI = _myAI;
    }

    // Evaluates the node, setting a new random target position for the AI within the specified range.
    public override BTNodeState Evaluate()
    {
        myAI.SetTarget(null); // Clears any current target.
        Debug.Log("finding wander point"); // Logs the action of finding a new wander point.
        myAI.SetTargetPosition(Random.insideUnitSphere * range); // Sets a new random target position within the range.
        return BTNodeState.SUCCESS; // Returns success after setting the new target position.
    }

}