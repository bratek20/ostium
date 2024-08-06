using System.Collections.Generic;
using B20.Events.Api;
using B20.Events.Impl;
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
                ListUtils.Of<WindowView>(
                    mainWindow,
                    gameWindow
                )
            );

            var eventPublisher = new EventPublisherLogic(ListUtils.Of<EventListener>());
            
            WindowManager windowManager = new WindowManagerLogic(windowManipulator);
            logic = new OstiumLogic(eventPublisher, windowManager);
            logic.RegisterWindows();
            
            mainWindow.Init(windowManager.Get(WindowIds.MAIN_WINDOW));
            gameWindow.Init(windowManager.Get(WindowIds.GAME_WINDOW));
            
            logic.Start();
        }
    }
}

