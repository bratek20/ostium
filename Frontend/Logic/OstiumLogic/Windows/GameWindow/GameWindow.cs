using B20.Events.Api;
using B20.Frontend.Windows.Api;
using GameModule.Api;

namespace Ostium.Logic
{
    public class GameWindow : Window
    {
        public WindowId GetId()
        {
            return WindowIds.GAME_WINDOW;
        }
        
        public GameVM Game { get; }

        public GameWindow(GameVM game)
        {
            Game = game;
        }

        public void OnOpen()
        {
            Game.StartGame();
        }
    }
}
