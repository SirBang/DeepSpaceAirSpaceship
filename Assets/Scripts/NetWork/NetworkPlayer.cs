using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// This class represents a networked player in a multiplayer game using the Fusion networking library.
public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    // Static reference to the local player instance. Allows easy access from other scripts.
    public static NetworkPlayer Local { get; set; }

    // Start is called before the first frame update. Here, it finds and destroys the initial camera.
    void Start()
    {

    }

    // Called when the player object is spawned in the networked game.
    public override void Spawned()
    {
        // Checks if this object is controlled by the local player.
        if (Object.HasInputAuthority)
        {
            Local = this; // Sets the static Local reference to this instance.

            Debug.Log("Spawned local player");
        }
        else Debug.Log("Spawned remote player"); // Logs spawning of remote players for debugging.
    }

    // This method is called when a player leaves the game.
    public void PlayerLeft(PlayerRef player)
    {
        // Checks if the leaving player is the one controlling this object.
        if (player == Object.InputAuthority)
            Runner.Despawn(Object); // Despawns this object from the networked game.

    }
}