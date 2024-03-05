using System.Collections.Generic; 
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPlayer playerPrefab; // Reference to the player prefab to spawn for each connected player

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can be added here if needed
    }

    // Called when a player joins the game. Responsible for spawning player entities.
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer) // Check if this instance is the server
        {
            Debug.Log("OnPlayerJoined we are server. Spawning player");
            // Spawn the playerPrefab at the origin with no rotation and associate it with the joining player
            runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
        }
        else Debug.Log("OnPlayerJoined but not server");
    }

    // Other callback methods from INetworkRunnerCallbacks interface
    // These methods are currently empty or only contain logging but can be implemented as needed for game logic

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        // Called when input is received from any player
    }

    public void OnConnectedToServer(NetworkRunner runner) { Debug.Log("OnConnectedToServer"); }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { /* Called when a player leaves the game */ }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { /* Called when expected input from a player is missing */ }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { Debug.Log("OnShutdown"); }
    public void OnDisconnectedFromServer(NetworkRunner runner) { Debug.Log("OnDisconnectedFromServer"); }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { Debug.Log("OnConnectRequest"); }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { Debug.Log("OnConnectFailed"); }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { /* Called for custom simulation messages */ }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { /* Called when the list of available sessions is updated */ }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { /* Called upon receiving a custom authentication response */ }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { /* Called during host migration process */ }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { /* Called when reliable data is received from a player */ }
    public void OnSceneLoadDone(NetworkRunner runner) { /* Called when a scene load operation is completed */ }
    public void OnSceneLoadStart(NetworkRunner runner) { /* Called when a scene load operation starts */ }
}