using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour, IControllerInput, IBehaviourAI
{
    // Events for AI inputsdddd
    public event InputEventFloat ForwardEvent;
    public event InputEventFloat YawEvent;
    public event InputEventFloat PitchEvent;
    public event InputEventFloat RollEvent;
    public event InputEventFloat SideStrafeEvent;
    public event InputEventFloat VerticalStrafeEvent;
    public event InputEventFloat SlideEvent;
    public event InputEventVector3 TurnEvent;

    // Target position for the AI to move towards
    public Vector3 myTargetPosition = Vector3.zero;

    // Behavior tree nodes
    public Selector rootAI;
    public Sequence CheckArrivalSequence;
    public Sequence MoveSequence;
    bool avoiding = false; // Flag to indicate if the AI is currently
    public float avoidDistance = 200f; // Distance to check for obstacles to avoid
    Vector3 temporaryTarget; // Temporary target position when av
    Vector3 savedTargetPosition; // Original target positi
    GameObject target = null; // The current target object

    public bool movement_state = true;
    // Start is called before the first fram
    void Start()
    {
        // Initializ
        CheckArrivalSequence = new Sequence(new List<BTNode>
        {
            new CheckArrivalTask(this),
            new FindWanderPointTask (this, 600f),
        });

        MoveSequence = new Sequence(new List<BTNode>
        {
            new ObstacleAvoidance(this, avoidDistance, TurnEvent),
            new MoveToTargetTask(this, 300f, ForwardEvent),
            //  new ObstacleAvoidance(this, avoidDistance, TurnEvent),
            // new MoveToTargetTask(this, 300f, ForwardEvent),
        });

        // Root behavior selector
        rootAI = new Selector(new List<BTNode>
        {
            CheckArrivalSequence,
            MoveSequence,
        });

        // Redundant instantiation of FindWanderPointTask
        new FindWanderPointTask(this, 600f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            movement_state = !movement_state;
        // Evaluate the root behavior tree node
        if(movement_state)
        {  
            rootAI.Evaluate();
        }
    }

    // Set the target position for the AI
    public Vector3 SetTargetPosition(Vector3 targetPosition)
    {
        myTargetPosition = targetPosition;
        return myTargetPosition;

    }

    // Get the transform of the AI agent
    public Transform GetAgentTransform()
    {
        return transform;
    }

    // Get the current target position
    public Vector3 GetTargetPosition()
    {
        if (target != null) return target.transform.position;

        return myTargetPosition;
    }

    // Set a new target object for the AI
    public GameObject SetTarget(GameObject newTarget)
    {
        target = newTarget;
        return target;
    }

    // Get the current target object
    public GameObject GetTarget()
    {
        return target;
    }

    // Get the transform of the AI game object
    public Transform GetTransform()
    {
        return gameObject.transform;
    }

    // Get the avoiding flag status
    public bool GetAvoidFlag()
    {
        return avoiding;
    }

    // Set a temporary target position for obstacle avoidance
    public Vector3 SetTempTarget(Vector3 position)
    {
        avoiding = true;
        temporaryTarget = position;
        savedTargetPosition = myTargetPosition;
        return position;
    }

    // Return to the saved target position after avoiding obstacles
    public Vector3 ReturnToSaveTarget()
    {
        avoiding = false;
        temporaryTarget = Vector3.zero;
        myTargetPosition = savedTargetPosition;

        return savedTargetPosition;
    }
}