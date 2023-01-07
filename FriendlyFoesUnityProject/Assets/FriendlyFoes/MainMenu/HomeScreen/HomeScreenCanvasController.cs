using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FriendlyFoes.MainMenu.HomeScreen
{
    public class HomeScreenCanvasController : MainMenuCanvasController
    {
        [SerializeField]
        private Button _playButton = null;
        [SerializeField]
        private Button _optionsButton = null;
        [SerializeField]
        private Button _quitButton = null;

        public Button playButton => _playButton;
        public Button optionsButton => _optionsButton;
        public Button quitButton => _quitButton;
    }
}
