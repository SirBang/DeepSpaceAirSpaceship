using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the interface for AI behavior in a Unity game. 
/// This interface outlines the essential functionalities that an AI character or object should possess for navigation and interaction within the game world.
/// </summary>
public interface IBehaviourAI 
{
    /// <summary>
    /// Sets the target position for the AI to navigate towards.
    /// </summary>
    /// <param name="targetPosition">The target position in the game world.</param>
    /// <returns>The target position.</returns>
    Vector3 SetTargetPosition(Vector3 targetPosition);

    /// <summary>
    /// Retrieves the transform of the AI agent.
    /// </summary>
    /// <returns>The transform of the AI agent.</returns>
    Transform GetAgentTransform();

    /// <summary>
    /// Gets the current target position the AI is navigating towards.
    /// </summary>
    /// <returns>The current target position.</returns>
    Vector3 GetTargetPosition();

    /// <summary>
    /// Sets the target game object for the AI to interact with or navigate towards.
    /// </summary>
    /// <param name="gameObject">The target game object.</param>
    /// <returns>The target game object.</returns>
    GameObject SetTarget(GameObject gameObject);

    /// <summary>
    /// Gets the current target game object the AI is set to interact with or navigate towards.
    /// </summary>
    /// <returns>The current target game object.</returns>
    GameObject GetTarget();

    /// <summary>
    /// Retrieves the transform of the AI object itself.
    /// </summary>
    /// <returns>The transform of the AI object.</returns>
    Transform GetTransform();

    /// <summary>
    /// Checks if the AI should avoid obstacles while navigating.
    /// </summary>
    /// <returns>True if obstacle avoidance is enabled, false otherwise.</returns>
    bool GetAvoidFlag();

    /// <summary>
    /// Temporarily sets a target position for the AI, usually for avoiding obstacles or taking detours.
    /// </summary>
    /// <param name="position">The temporary target position.</param>
    /// <returns>The temporary target position.</returns>
    Vector3 SetTempTarget(Vector3 position);

    /// <summary>
    /// Returns the AI to its original target position after a temporary detour.
    /// </summary>
    /// <returns>The original target position before the detour.</returns>
    Vector3 ReturnToSaveTarget();
}