using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : BTNode
{
    IBehaviourAI myAI;
    Transform agentTransform;
    float avoidDistance;
    event InputEventVector3 TurnEvent;

    // Constructor initializes the AI, the distance to check for obstacles, and the event to trigger when turning.
    public ObstacleAvoidance(IBehaviourAI _myAI, float _avoidDistance, InputEventVector3 _turnEvent)
    {
        myAI = _myAI;
        avoidDistance = _avoidDistance;
        TurnEvent = _turnEvent;
    }

    // Evaluate method is called to perform the obstacle avoidance behavior.
    public override BTNodeState Evaluate()
    {
        // Get the current transform of the AI agent.
        agentTransform = myAI.GetAgentTransform();
        Vector3[] rayDirections =
        {
            agentTransform.forward,
            Helper.GetDirectionFromAngleInDegrees(10f, agentTransform.forward, agentTransform.right),
            Helper.GetDirectionFromAngleInDegrees(-10f, agentTransform.forward, agentTransform.right),
            Helper.GetDirectionFromAngleInDegrees(10f, agentTransform.forward, agentTransform.up),
            Helper.GetDirectionFromAngleInDegrees(-10f, agentTransform.forward, agentTransform.up),
            (agentTransform.forward + agentTransform.right). normalized,
            (agentTransform.forward - agentTransform.right). normalized,
            (agentTransform.forward + agentTransform.up). normalized,
            (agentTransform.forward - agentTransform.up). normalized,
            (agentTransform.right). normalized,
            (-agentTransform.right). normalized,
            (agentTransform.up). normalized,
            (-agentTransform.up). normalized,
        };

        DrawRays(rayDirections);
        // Iterate through each ray direction to check for obstacles.
        for (int i = 0; i < rayDirections.Length; i++)
        {
            RaycastHit hit;

            // Perform a raycast in the current direction to detect obstacles.
            if (Physics.Raycast(agentTransform.position, rayDirections[i], out hit, avoidDistance))
            {
                // If an obstacle is detected and it's tagged as "Enemy", try to find a safe direction.
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    // Iterate through ray directions to find a safe direction that doesn't hit an obstacle.
                    for (int j = 0; j < rayDirections.Length; j++)
                    {
                        if (!Physics.Raycast(agentTransform.position, rayDirections[j] * (avoidDistance / 4), out hit, avoidDistance))
                        {
                            // A safe direction is found, calculate the new heading and trigger the TurnEvent.
                            Vector3 newHeading = rayDirections[j];
                            Vector3 newTarget = -agentTransform.position + (newHeading * (avoidDistance / 4));
                            TurnEvent(newHeading.x, newHeading.y, newHeading.z);
                            return BTNodeState.SUCCESS;
                        }
                    }
                }
            }
        }

        // If no obstacles are detected, or no safe direction is found, default to moving towards the target.
        Vector3 agentPosition = agentTransform.position;
        Vector3 targetPosition = myAI.GetTargetPosition();
        Vector3 desiredHeading = (targetPosition - agentPosition).normalized;

        // Trigger the TurnEvent to move towards the target.
        TurnEvent(desiredHeading.x, desiredHeading.y, desiredHeading.z);

        return BTNodeState.SUCCESS;
    }

    private void DrawRays (Vector3[] rayDirections)
    {
        foreach (Vector3 dir in rayDirections)
        {
            Debug.DrawRay(agentTransform.position, dir * avoidDistance, Color.blue);
        }
    }

}