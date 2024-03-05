using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    // Defines a structure for networked player input in a multiplayer game.
    // This structure includes:
    // - rotationInput: A float representing the player's input for rotation.
    // - movementInput: A Vector2 representing the player's input for movement in two dimensions (e.g., forward/backward, left/right).
    // - isJumpPressed: A NetworkBool indicating whether the jump action has been initiated by the player.
    public float rotationInput;
    public Vector2 movementInput;
    public NetworkBool isJumpPressed;
}