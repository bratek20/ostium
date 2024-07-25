using Logic;
using Logic.Core;
using Ostium.Logic;
using OstiumLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class GameManager : MonoBehaviour, GameStateListener
    {
        [SerializeField]
        private WindowView mainWindow;
        [SerializeField]
        private WindowView gameWindow;
        [SerializeField]
        private ButtonView playButton;

        private GameState state;

        void Start()
        {
            int x = new Class1().PrintHelloWorld();
            Debug.Log("x:" + x);

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
            }
        }
    }
}

