using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab; // Prefab reference for NetworkRunner to be instantiated.
    public Transform[] tempPositions; // Temporary positions array, potentially for spawning or testing.

    NetworkRunner networkRunner; // Instance of NetworkRunner used in this handler.

    void Start()
    {
        networkRunner = Instantiate(networkRunnerPrefab); // Instantiate the NetworkRunner prefab.
        networkRunner.name = "Network runner"; // Set a name for the instantiated NetworkRunner object.

        // Initialize the network runner with default settings and log the start.
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
        Debug.Log($"Server NetworkRunner started.");
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
    {
        // Attempt to find an existing INetworkSceneObjectProvider component or add a new one if not found.
        var sceneObjectProvider = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneObjectProvider>().FirstOrDefault();
        
        Debug.Log(address); // Log the network address being used.

        if (sceneObjectProvider == null)
        {
            // If no INetworkSceneObjectProvider is found, add the default one to handle networked scene objects.
            sceneObjectProvider = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true; // Enable input provision for the runner.

        // Start the game with specified settings including game mode, network address, scene, session name, and the scene object provider.
        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = "TestRoom",
            Initialized = initialized,
            SceneObjectProvider = sceneObjectProvider
        });
    }
}