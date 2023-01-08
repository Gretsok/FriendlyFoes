using System;
using UnityEngine;

namespace FriendlyFoes.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager.NetworkManager _networkManagerPrefab = null;
        private NetworkManager.NetworkManager _networkManager = null;

        [SerializeField]
        private NetworkManager.Controls.PlayerInputManagerController _inputManagerControllerPrefab = null;
        private NetworkManager.Controls.PlayerInputManagerController _inputManagerController = null;
        public NetworkManager.Controls.PlayerInputManagerController inputManagerController => _inputManagerController;



        private void Awake()
        {
            _inputManagerController = FindObjectOfType<NetworkManager.Controls.PlayerInputManagerController>();

            if(!_inputManagerController)
            {
                _inputManagerController = Instantiate(_inputManagerControllerPrefab);
            }
            _inputManagerController.EnablePlayerRegistration();
            
        }

        private void OnDestroy()
        {
            _inputManagerController.DisablePlayerRegistration();
        }

        public void StartGameAsHost(string sessionName)
        {
            if(_networkManager)
            {
                Destroy(_networkManager.gameObject);
            }

            _networkManager = Instantiate(_networkManagerPrefab);
            _networkManager.StartGame(Fusion.GameMode.Host, sessionName);
        }

        public void StartGameAsClient(string sessionName)
        {
            if (_networkManager)
            {
                Destroy(_networkManager.gameObject);
            }

            _networkManager = Instantiate(_networkManagerPrefab);
            _networkManager.StartGame(Fusion.GameMode.Client, sessionName);
        }
    }
}