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
        
        public Game Game { get; private set; }
    
        public GameWindow(GameSetupApi gameSetupApi)
        {
            Game = gameSetupApi.StartGame();
        }
    }
}
