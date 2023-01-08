using FriendlyFoes.NetworkManager.Controls;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace FriendlyFoes.MainMenu.PlayerMonitoringScreen
{
    public class PlayerInfoWidget : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_nameText = null;
        [SerializeField]
        private Image m_borderImage = null;

        private PlayerInput _playerInput = null;

        public void SetPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            if(_playerInput)
            {
                var data = playerInput.GetComponent<PlayerInputData>();
                m_nameText.text = data.playerState.name.Value;
                m_borderImage.color = data.playerState.color;
            }
        }
    }
}