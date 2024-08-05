using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Frontend.Windows.Integrations;
using B20.Logic;
using B20.View;
using GameSetup.Api;
using GameSetup.Impl;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private WindowView mainWindow;
        [SerializeField]
        private GameWindowView gameWindow;
        [SerializeField]
        private ButtonView playButton;

        private OstiumLogic logic;

        void Start()
        {
            mainWindow.Init(new MainWindow());
            gameWindow.Init(new GameWindow());

            var windowManipulator = new UnityWindowManipulator();
            WindowManager windowManager = new WindowManagerLogic(windowManipulator);
            logic = new OstiumLogic(new WindowManagerLogic(windowManipulator));

            playButton.Init(new PlayButton(windowManager));
        }
    }
}

