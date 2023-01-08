using UnityEngine;

namespace FriendlyFoes.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager.NetworkManager _networkManagerPrefab = null;
        private NetworkManager.NetworkManager _networkManager = null;
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