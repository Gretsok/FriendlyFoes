using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FriendlyFoes.MainMenu.PlayerMonitoringScreen
{
    public class PlayerMonitoringScreenCanvasController : MainMenuCanvasController
    {
        [SerializeField]
        private Transform _widgetContainer = null;
        [SerializeField]
        private PlayerInfoWidget _widgetPrefab = null;

        private List<PlayerInfoWidget> _instantiatedWidgets = new List<PlayerInfoWidget>();

        public void SetUpWidgets(List<PlayerInput> playerInputs)
        {
            for(int i = _instantiatedWidgets.Count - 1; i >= 0; --i)
            {
                Destroy(_instantiatedWidgets[i].gameObject);
            }

            _instantiatedWidgets.Clear();

            for(int i = 0; i < playerInputs.Count; ++i)
            {
                var newWidget = Instantiate(_widgetPrefab, _widgetContainer);
                newWidget.SetPlayerInput(playerInputs[i]);
                _instantiatedWidgets.Add(newWidget);
            }
        }
    }
}