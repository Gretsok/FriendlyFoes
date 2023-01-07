using UnityEngine;

namespace FriendlyFoes.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager.NetworkManager _networkManager = null;
        public void StartGameAsHost(string sessionName)
        {
            _networkManager.StartGame(Fusion.GameMode.Host, sessionName);
        }

        public void StartGameAsClient(string sessionName)
        {
            _networkManager.StartGame(Fusion.GameMode.Client, sessionName);
        }
    }
}