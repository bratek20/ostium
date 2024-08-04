using B20.Logic;
using B20.View;
using GameSetup.Api;
using GameSetup.Impl;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class GameManager : MonoBehaviour, GameStateListener
    {
        [SerializeField]
        private WindowView mainWindow;
        [SerializeField]
        private GameWindowView gameWindow;
        [SerializeField]
        private ButtonView playButton;

        private GameState state;

        void Start()
        {
            mainWindow.Init(new MainWindow());
            gameWindow.Init(new GameWindow());

            state = new GameState(this, mainWindow.Value);

            playButton.Init(new PlayButton(state, gameWindow.Value as GameWindow));

            UpdateView();
        }

        public void OnStateChanged()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            if (state.CurrentWindow == mainWindow.Value)
            {
                mainWindow.SetActive(true);
                gameWindow.SetActive(false);
            }
            if (state.CurrentWindow == gameWindow.Value)
            {
                mainWindow.SetActive(false);
                gameWindow.SetActive(true);

                GameSetupApi api = new GameSetupApiLogic();
                gameWindow.Init2(api.StartGame());
            }
        }
    }
}

