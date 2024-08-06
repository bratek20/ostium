using B20.Events.Api;
using B20.Frontend.Windows.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class GameWindow : Window
    {
        public WindowId GetId()
        {
            return WindowIds.GAME_WINDOW;
        }



        public GameVM Game { get; private set; }

        private GameSetupApi gameSetupApi;
        
        public GameWindow(EventPublisher eventPublisher, GameSetupApi gameSetupApi)
        {
            Game = new GameVM(eventPublisher);
            this.gameSetupApi = gameSetupApi;
        }

        public void OnOpen()
        {
            Game.Update(gameSetupApi.StartGame());
        }
    }
}
