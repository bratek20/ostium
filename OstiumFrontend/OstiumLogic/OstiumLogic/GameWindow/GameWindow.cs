using B20.Events.Api;
using B20.Frontend.Elements.Api;
using B20.Frontend.Windows.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class GameWindowListener : EventListener<PanelClickedEvent>
    {
        public void HandleEvent(PanelClickedEvent e)
        {
            if (e.Panel is CreatureCardVM)
            {
                System.Console.WriteLine("Creature card clicked");
            }
            if (e.Panel is RowVM)
            {
                System.Console.WriteLine("Row clicked");
            }
        }
    }
    
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
            eventPublisher.AddListener(new GameWindowListener());
            this.gameSetupApi = gameSetupApi;
        }

        public void OnOpen()
        {
            Game.Update(gameSetupApi.StartGame());
        }
    }
}
