using UnityEngine;

namespace FriendlyFoes.MainMenu
{
    public class MainMenuCanvasManager : MonoBehaviour
    {
        [Header("Scene Refs")]
        [SerializeField]
        private MainMenuManager _mainMenuManager = null;

        [Header("Components Refs")]
        [SerializeField]
        private HomeScreen.HomeScreenCanvasController _homeScreenCanvas = null;
        [SerializeField]
        private JoinServerScreen.JoinServerScreenCanvasController _joinServerScreenCanvas = null;

        private void Awake()
        {
            _homeScreenCanvas.playButton.onClick.AddListener(HandlePlayButtonClicked);
            _homeScreenCanvas.optionsButton.onClick.AddListener(HandleOptionsButtonClicked);
            _homeScreenCanvas.quitButton.onClick.AddListener(HandleQuitButtonClicked);

            _joinServerScreenCanvas.joinButton.onClick.AddListener(HandleJoinButtonClicked);
            _joinServerScreenCanvas.hostButton.onClick.AddListener(HandleHostButtonClicked);
            _joinServerScreenCanvas.backButton.onClick.AddListener(HandleBackButtonClicked);
        }

        private void Start()
        {
            ActivateHomeScreen();
        }

        public void ActivateHomeScreen()
        {
            _homeScreenCanvas.Show();
            _joinServerScreenCanvas.Hide();
        }

        public void ActivateJoinServerScreen()
        {
            _homeScreenCanvas.Hide();
            _joinServerScreenCanvas.Show();
        }

        #region Buttons callbacks
        #region Home Screen
        private void HandlePlayButtonClicked()
        {
            ActivateJoinServerScreen();
        }

        private void HandleOptionsButtonClicked()
        {
            Debug.Log("Options button clicked");
        }

        private void HandleQuitButtonClicked()
        {
            Application.Quit();
        }
        #endregion

        #region Join Server Screen
        private void HandleBackButtonClicked()
        {
            ActivateHomeScreen();
        }

        private void HandleHostButtonClicked()
        {
            _mainMenuManager.StartGameAsHost(_joinServerScreenCanvas.sessionNameField.text);
        }

        private void HandleJoinButtonClicked()
        {
            _mainMenuManager.StartGameAsClient(_joinServerScreenCanvas.sessionNameField.text);
        }
        #endregion
        #endregion
    }
}