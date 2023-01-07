using UnityEngine;
using UnityEngine.UI;

namespace FriendlyFoes.MainMenu.JoinServerScreen
{
    public class JoinServerScreenCanvasController : MainMenuCanvasController
    {
        [SerializeField]
        private Button _joinButton = null;
        [SerializeField]
        private Button _hostButton = null;
        [SerializeField]
        private Button _backButton = null;
        [SerializeField]
        private TMPro.TMP_InputField _sessionNameField = null;

        public Button joinButton => _joinButton;
        public Button hostButton => _hostButton;
        public Button backButton => _backButton;
        public TMPro.TMP_InputField sessionNameField => _sessionNameField;
    }
}