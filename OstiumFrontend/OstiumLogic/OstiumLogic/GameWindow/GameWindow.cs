using B20.Events.Api;
using B20.Frontend.Elements;
using B20.Frontend.Windows.Api;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class GameWindowListener : EventListener<PanelClickedEvent>
    {
        private GameSetupApi gameSetupApi;
        private GameVM game;
        public GameWindowListener(GameSetupApi gameSetupApi, GameVM game)
        {
            this.gameSetupApi = gameSetupApi;
            this.game = game;
        }
        
        private CreatureCardId clickedCardId;
        public void HandleEvent(PanelClickedEvent e)
        {
            if (e.Panel is CreatureCardVM card)
            {
                clickedCardId = card.Model.GetId();
            }
            if (e.Panel is RowVM row)
            {
                var type = row.Type;
                var gameModel = gameSetupApi.PlayCard(clickedCardId, type);
                game.Update(gameModel);
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
            eventPublisher.AddListener(new GameWindowListener(gameSetupApi, Game));
            this.gameSetupApi = gameSetupApi;
        }

        public void OnOpen()
        {
            Game.Update(gameSetupApi.StartGame());
        }
    }
}
