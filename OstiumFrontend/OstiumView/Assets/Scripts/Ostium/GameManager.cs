using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Frontend.Windows.Integrations;
using B20.Logic.Utils;
using B20.View;
using Ostium.Logic;
using Ostium.View.MainWindow;
using UnityEngine;

namespace Ostium.View
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private MainWindowView mainWindow;
        [SerializeField]
        private GameWindowView gameWindow;

        private OstiumLogic logic;

        void Start()
        {
            var windowManipulator = new UnityWindowManipulator(
                ListUtils.ListOf<WindowView>(
                    mainWindow,
                    gameWindow
                )
            );
            
            WindowManager windowManager = new WindowManagerLogic(windowManipulator);
            logic = new OstiumLogic(windowManager);
            logic.RegisterWindows();
            
            mainWindow.Init(windowManager.Get(WindowIds.MAIN_WINDOW));
            gameWindow.Init(windowManager.Get(WindowIds.GAME_WINDOW));
            
            logic.Start();
        }
    }
}

