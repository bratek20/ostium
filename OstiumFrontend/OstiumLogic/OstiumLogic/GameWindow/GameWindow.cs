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
    
        public GameWindow(EventPublisher eventPublisher, GameSetupApi gameSetupApi)
        {
            Game = new GameVM(eventPublisher);
            
            Game.Update(gameSetupApi.StartGame());
        }
    }
}
