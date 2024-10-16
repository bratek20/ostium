using System;
using System.Collections.Generic;
using B20.Ext;
using B20.Frontend.Windows.Api;
using B20.Frontend.UiElements;
using KaijuGame.Api;
using GamesManagement.Api;

namespace KaijuGame.ViewModel {
    public partial class GameWindow
    {
        private GameApi api;

        public GameWindow(GameApi api)
        {
            this.api = api;
        }

        protected override void OnOpen()
        {
            GameState.Update(api.GetState(State.Token));
        }
    }
}