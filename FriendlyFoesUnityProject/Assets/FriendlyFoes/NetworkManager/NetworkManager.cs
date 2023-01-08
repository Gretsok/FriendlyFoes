using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FriendlyFoes.NetworkManager
{
    public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
    {
        private NetworkRunner _runner;
        private States.NetworkStatesManager _statesManager = null;
        private Controls.PlayerInputManagerController _inputManagerController = null;

        [SerializeField] private Controls.NetworkSceneControls _sceneControls;
        private Dictionary<PlayerRef, List<Controls.NetworkSceneControls>> _spawnedCharacters = new Dictionary<PlayerRef, List<Controls.NetworkSceneControls>>();
        [SerializeField]
        private States.NetworkStatesManager _statesManagerPrefab = null;

        private bool _isInitialized = false;

        private void Awake()
        {
            _inputManagerController = FindObjectOfType<Controls.PlayerInputManagerController>();
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                if(!_isInitialized)
                {
                    _statesManager = runner.Spawn(_statesManagerPrefab);
                }


                List<Controls.NetworkSceneControls> playersSceneControls = new List<Controls.NetworkSceneControls>();
                // Create a unique position for the player
                for(int i = 0; i < _inputManagerController.playerCount; ++i)
                {
                    Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, i);
                    Controls.ANetworkCharacter networkPlayerCharacter =
                        runner.Spawn(_sceneControls.character, spawnPosition, Quaternion.identity);
                    Controls.ANetworkInputController networkInputController =
                        runner.Spawn(_sceneControls.inputController, default, default, player);

                    networkInputController.Possess(networkPlayerCharacter);
                    networkInputController.localPlayerIndex = i;

                    Controls.NetworkSceneControls newPlayerSceneControls;
                    newPlayerSceneControls.character = networkPlayerCharacter;
                    newPlayerSceneControls.inputController = networkInputController;

                    playersSceneControls.Add(newPlayerSceneControls);
                }
                

                // Keep track of the player avatars so we can remove it when they disconnect
                _spawnedCharacters.Add(player, playersSceneControls);

                _statesManager.HandleNewPlayerConnected(player);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            // Find and remove the players avatar
            if (_spawnedCharacters.TryGetValue(player, out List<Controls.NetworkSceneControls> playersSceneControls))
            {
                for(int i = playersSceneControls.Count - 1; i <= 0; --i)
                {
                    playersSceneControls[i].inputController.Possess(null);
                    runner.Despawn(playersSceneControls[i].inputController.Object);
                    runner.Despawn(playersSceneControls[i].character.Object);
                    playersSceneControls.RemoveAt(i);
                }
                _spawnedCharacters.Remove(player);
                
            }

            _statesManager.HandlePlayerDisconnected(player);
        }
        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var data = new NetworkInputData();
            for(int i = 0; i < _inputManagerController.playerCount; ++i)
            {
                NetworkInputData.PlayerData playerData = new NetworkInputData.PlayerData();
                playerData.directionInput = _inputManagerController.GetPlayerInput(i).
                    actions.FindActionMap("Hub").FindAction("Movement").ReadValue<Vector2>();
                data.playerData.Set(i, playerData);
            }
            input.Set(data);
        }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) 
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
                SceneManager.LoadSceneAsync(0);
            ClearManager();
        }
        public void OnConnectedToServer(NetworkRunner runner) 
        {
        }
        public void OnDisconnectedFromServer(NetworkRunner runner) 
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
                SceneManager.LoadSceneAsync(0);
            ClearManager();
        }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
                SceneManager.LoadSceneAsync(0);
            ClearManager();
        }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }

        public async void StartGame(GameMode mode, string sessionName = "TestRoom")
        {
            if(_runner != null)
            {
                Debug.LogError("Cannot start game if the runner already exist");
                return;
            }

            // Create the Fusion runner and let it know that we will be providing user input
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            // Start or join (depends on gamemode) a session with a specific name
            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = sessionName,
                Scene = 1,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }

        private void ClearManager()
        {
            Destroy(_runner);
            Destroy(GetComponent<NetworkSceneManagerDefault>());
            if(_statesManager)
                Destroy(_statesManager.gameObject);
        }
    }
}