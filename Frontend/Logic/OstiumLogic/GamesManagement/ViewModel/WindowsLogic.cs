using B20.Events.Api;
using B20.Frontend.Traits;
using B20.Frontend.Windows.Api;
using GamesManagement.Api;
using SingleGame.ViewModel;

namespace GamesManagement.ViewModel
{
    class CreatedGameClickedListener: EventListener<UiElementClickedEvent>
    {
        private GamesManagementWindow window;
        public CreatedGameClickedListener(GamesManagementWindow window)
        {
            this.window = window;
        }
        
        public void HandleEvent(UiElementClickedEvent e)
        {
            if (e.Element is CreatedGameVm game)
            {
                window.HandleCreatedGameClicked(game);
            }
        }
    }
    
    public partial class GamesManagementWindow
    {
        private GamesManagementApi api;
        private WindowManager windowManager;

        public GamesManagementWindow(GamesManagementApi api, WindowManager windowManager, EventPublisher eventPublisher)
        {
            this.api = api;
            this.windowManager = windowManager;
            
            eventPublisher.AddListener(new CreatedGameClickedListener(this));
        }
        
        public void HandleCreatedGameClicked(CreatedGameVm game)
        {
            api.Join(State.Username, game.Model.GetId());
            windowManager.Open<GameWindow, GameWindowState>(new GameWindowState(State.Username, game.Model.GetId()));
        }

        protected override void OnOpen()
        {
            CreateGame.OnClick(OnCreateClicked);
            CreatedGames.OnElementCreated(createdGame => createdGame.Delete.OnClick(() => HandleCreatedGameDeleteButtonClicked(createdGame)));
            
            Refresh();
        }
        
        private void Refresh()
        {
            CreatedGames.Update(api.GetAllCreated());
        }
        
        private void OnCreateClicked()
        {
            var gameId = api.Create(State.Username);
            windowManager.Open<GameWindow, GameWindowState>(new GameWindowState(State.Username, gameId));
        }
        
        private void HandleCreatedGameDeleteButtonClicked(CreatedGameVm game)
        {
            api.Delete(game.Model.GetId());
            Refresh();
        }
    }
}