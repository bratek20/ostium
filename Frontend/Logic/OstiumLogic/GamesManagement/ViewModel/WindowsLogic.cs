using B20.Frontend.Windows.Api;
using GamesManagement.Api;
using SingleGame.ViewModel;

namespace GamesManagement.ViewModel
{
    public partial class GamesManagementWindow
    {
        private GamesManagementApi api;
        private WindowManager windowManager;

        public GamesManagementWindow(GamesManagementApi api, WindowManager windowManager)
        {
            this.api = api;
            this.windowManager = windowManager;
        }

        protected override void OnOpen()
        {
            CreateGame.OnClick(OnCreateClicked);
            
            CreatedGames.Update(api.GetAllCreated());
        }
        
        private void OnCreateClicked()
        {
            var gameId = api.Create(State.Username);
            windowManager.Open<GameWindow, GameWindowState>(new GameWindowState(State.Username, gameId));
        }
    }
}