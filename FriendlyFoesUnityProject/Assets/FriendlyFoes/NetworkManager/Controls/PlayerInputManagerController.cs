using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FriendlyFoes.NetworkManager.Controls
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerInputManagerController : MonoBehaviour
    {
        private PlayerInputManager _inputManager = null;

        private List<PlayerInput> _playerInputs = new List<PlayerInput>();
        public List<PlayerInput> playerInputs => _playerInputs;
        public int playerCount => _inputManager.playerCount;

        public Action onPlayerCountUpdated = null;

        [Header("Default Data")]
        [SerializeField]
        private List<States.PlayerState> _defaultPlayerStates = null;

        private void Awake()
        {
            _inputManager = GetComponent<PlayerInputManager>();
            DontDestroyOnLoad(gameObject);

            _inputManager.onPlayerJoined += HandlePlayerJoined;
            _inputManager.onPlayerLeft += HandlePlayerLeft;
        }

        private void OnDestroy()
        {
            _inputManager.onPlayerJoined -= HandlePlayerJoined;
            _inputManager.onPlayerLeft -= HandlePlayerLeft;
        }

        public void EnablePlayerRegistration()
        {
            if (!_inputManager) return;

            _inputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
        }

        public void DisablePlayerRegistration()
        {
            if (!_inputManager) return;

            _inputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        }

        public PlayerInput GetPlayerInput(int index)
        {
            return playerCount > index ? _playerInputs[index] : null;
        }

        #region private callbacks
        private void HandlePlayerJoined(PlayerInput playerInput)
        {
            playerInput.transform.SetParent(transform);

            playerInput.GetComponent<PlayerInputData>().playerState = _defaultPlayerStates[playerCount % _defaultPlayerStates.Count];

            _playerInputs.Add(playerInput);
            onPlayerCountUpdated?.Invoke();
        }

        private void HandlePlayerLeft(PlayerInput playerInput)
        {
            _playerInputs.Remove(playerInput);
            onPlayerCountUpdated?.Invoke();
        }
        #endregion
    }
}