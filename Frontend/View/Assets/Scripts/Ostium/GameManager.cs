using System.Collections.Generic;
using B20.Architecture.Contexts.Context;
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
            var c = ContextsFactory.CreateBuilder()
                .SetImplObject(mainWindow)
                .SetImplObject(gameWindow)
                .SetImpl<WindowManipulator, UnityWindowManipulator>()
                .WithModule(new OstiumLogicFullImpl())
                .Build();

            var windowManager = c.Get<WindowManager>();
            mainWindow.Init(windowManager.Get<Logic.MainWindow>());
            gameWindow.Init(windowManager.Get<GameWindow>());
            
            logic = c.Get<OstiumLogic>();
            logic.Start();
        }
    }
}

